﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using back_end.GoogleSearchConsole;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Webmasters.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.Logging.EventSource;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace back_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GSCController : ControllerBase
    {
        private readonly ConnectionStrings connString;

        private static readonly GoogleCredential credential = GoogleCredential
            .FromFile("credentials.json")
            .CreateScoped(new[]
            {
                Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });

        public GSCController(IOptions<ConnectionStrings> connStringsAccessor)
        {
            // Lets us get connection string from appsettings.json
            connString = connStringsAccessor.Value;
        }

        [Route("/CreateAnalysis")]
        [HttpPost("{id}")]
        public string CreateAnalysis([FromBody] RequestObject request)
        {
            SearchConsoleData dataResult = GetSearchConsoleData(request);

            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                // Output the ID from the newly generated analysis for later use
                var sqlQuery = "INSERT INTO Analyses (Customer, Url, CreatedDate) output INSERTED.Id " +
                               "VALUES (@Customer, @Url, @CreatedDate) ";

                conn.Open();

                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.Add("@Customer", SqlDbType.NVarChar).Value = request.customerName;
                cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = request.siteUrl;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = DateTime.UtcNow;

                var newId = (int) cmd.ExecuteScalar();

                // Datatable for quickly bulk inserting new analysis into DB.
                var table = new DataTable();
                var idColumn = new DataColumn("Id", typeof(int))
                {
                    DefaultValue = newId
                };

                table.Columns.Add(idColumn);
                table.Columns.Add("DataId", typeof(int));
                table.Columns.Add("Keyword", typeof(string));
                table.Columns.Add("SiteUrl", typeof(string));
                table.Columns.Add("Impressions", typeof(int));
                table.Columns.Add("Clicks", typeof(int));
                table.Columns.Add("CTR", typeof(double));
                table.Columns.Add("Position", typeof(int));
                table.Columns.Add("Category", typeof(string));
                table.Columns.Add("SubCategory1", typeof(string));
                table.Columns.Add("SubCategory2", typeof(string));
                table.Columns.Add("Intent", typeof(string));
                table.Columns.Add("Colour", typeof(string));

                List<string> keywords = new List<string>();

                // Populate datatable using data fetched from google.
                // Also adds all keywords to seperate list, for splitting later.
                foreach (var data in dataResult.Results)
                {
                    var row = table.NewRow();
                    row["Keyword"] = data.Keyword;
                    row["SiteUrl"] = data.Page;
                    row["Clicks"] = data.Clicks;
                    row["Impressions"] = data.Impressions;
                    row["CTR"] = data.Ctr;
                    row["Position"] = data.Position;
                    keywords.AddRange(data.Keyword.Split());

                    table.Rows.Add(row);
                }

                using var bulk = new SqlBulkCopy(conn);
                bulk.DestinationTableName = "AnalysisData";
                bulk.WriteToServer(table);

                var keywordsTable = new DataTable();
                keywordsTable.Columns.Add("Id", typeof(int));
                var analysisIdColumn = new DataColumn("AnalysisId", typeof(int))
                {
                    DefaultValue = newId
                };

                keywordsTable.Columns.Add(analysisIdColumn);
                keywordsTable.Columns.Add("Keyword", typeof(string));
                keywordsTable.Columns.Add("Count", typeof(int));

                // Remove all keywords length 2 or less, and return only unique keywords.
                var distinctKeywords = keywords.Distinct().ToList();
                distinctKeywords.RemoveAll(word => word.Length <= 2);
                Dictionary<string,int> keywordDictionary = distinctKeywords.ToDictionary(d => d, x => keywords.Count(s => x==s));

                foreach (var keyword in keywordDictionary)
                {
                    var row = keywordsTable.NewRow();
                    row["Keyword"] = keyword.Key;
                    row["Count"] = keyword.Value;
                    keywordsTable.Rows.Add(row);
                }

                bulk.DestinationTableName = "SplitKeywords";
                bulk.WriteToServer(keywordsTable);

                conn.Close();

                // SQL Server does a bit of formatting on floats/decimals, so we fetch the newly saved data
                // from the database, and serve it directly. Ensures same result every time.
                var responseData = GetAnalysis(conn, newId);

                responseData.NumberOfRows = responseData.Results.Count;
                responseData.AnalysisId = newId;
                responseData.CustomerName = request.customerName;
                responseData.SiteUrl = request.siteUrl;

                return JsonConvert.SerializeObject(responseData);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return JsonConvert.SerializeObject("No data found");
        }

        [Route("/analysis/{id}")]
        [HttpGet]
        public string GetAnalysis(int id)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var responseData = GetAnalysis(conn, id);

                var sqlQuery = "SELECT TOP 1 Id, Customer, Url, CreatedDate FROM Analyses WHERE Id = @Id";

                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    responseData.CustomerName = (string) reader["Customer"];
                    responseData.SiteUrl = (string) reader["Url"];
                }

                reader.Close();
                conn.Close();

                responseData.NumberOfRows = responseData.Results.Count;
                responseData.AnalysisId = id;

                return JsonConvert.SerializeObject(responseData);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return "No data found";
        }

        [Route("/analysis/all")]
        [HttpGet]
        public string GetAnalysis()
        {
            var analysesResultList = new List<AnalysisMeta>();
            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var sqlQuery =
                    "SELECT a.Id, Customer, Url, CreatedDate, count(ad.id)[NumberOfRows] FROM Analyses A INNER JOIN AnalysisData ad ON a.Id = ad.Id " +
                    "GROUP BY a.Id, Customer, Url, CreatedDate " +
                    "ORDER BY CreatedDate DESC";

                conn.Open();

                using SqlCommand cmd1 = new SqlCommand(sqlQuery, conn);
                var reader = cmd1.ExecuteReader();

                while (reader.Read())
                {
                    var analysis = new AnalysisMeta();
                    analysis.id = reader["Id"].ToString();
                    analysis.customerName = reader["Customer"].ToString();
                    analysis.url = reader["Url"].ToString();
                    analysis.createdDate = (DateTime) reader["CreatedDate"];
                    analysis.numberOfRows = (int) reader["NumberOfRows"];
                    analysesResultList.Add(analysis);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            return JsonConvert.SerializeObject(analysesResultList);
        }

        [Route("/analysis/category/{id}")]
        [HttpPut]
        public string PutCategory([FromBody] CategoryData categoryData, int id)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var sqlUpdate = "UPDATE AnalysisData " +
                                "SET Category = @Category, " +
                                "SubCategory1 = @SubCategory1, " +
                                "SubCategory2 = @SubCategory2, " +
                                "Intent = @Intent, " +
                                "Colour = @Colour " +
                                "WHERE DataId = @DataId";

                using SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = categoryData.category;
                cmd.Parameters.Add("@SubCategory1", SqlDbType.NVarChar).Value = categoryData.subCategory1;
                cmd.Parameters.Add("@SubCategory2", SqlDbType.NVarChar).Value = categoryData.subCategory2;
                cmd.Parameters.Add("@Intent", SqlDbType.NVarChar).Value = categoryData.intent;
                cmd.Parameters.Add("@Colour", SqlDbType.NVarChar).Value = categoryData.colour;
                cmd.Parameters.Add("@DataId", SqlDbType.Int).Value = id;

                conn.Open();
                var res = cmd.ExecuteNonQuery();

                conn.Close();
                if (res == 1)
                {
                    return $"Updated id: {id}";
                }
                else
                {
                    return "Something went wrong";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return $"Something went wrong: {e.Message}";
            }
        }

        [Route("/analysis/{id}/keywords")]
        [HttpGet]
        public string GetSplitKeywords(int id)
        {
            List<KeywordData> keywords = new List<KeywordData>();
            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var sqlQuery =
                    "SELECT AnalysisId, Keyword, Count FROM SplitKeywords  " +
                    "WHERE AnalysisId = @AnalysisId " +
                    "ORDER BY Count DESC";
                conn.Open();

                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.Add("@AnalysisId", SqlDbType.Int).Value = id;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var keyword = new KeywordData();
                    keyword.analysisId = (int) reader["AnalysisId"];
                    keyword.keyword = reader["Keyword"].ToString();
                    keyword.count = (int) reader["Count"];
                    keywords.Add(keyword);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return JsonConvert.SerializeObject(keywords);
        }

        [Route("/analysis/{id}/categories")]
        [HttpGet]
        public string GetCategoriesWithKeywords(int id)
        {
            Dictionary<string, int> categories = new Dictionary<string, int>();
            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var sqlQuery =
                    "SELECT Category, Count(Keyword)[Count] FROM AnalysisData " +
                    "WHERE Id = @Id " +
                    "GROUP BY Category " +
                    "ORDER BY Count DESC";
                conn.Open();

                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(reader["Category"].ToString(), (int)reader["Count"]);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            return JsonConvert.SerializeObject(categories);
        }

        private SearchConsoleData GetAnalysis(SqlConnection conn, int id)
        {
            var dataResult = new SearchConsoleData
            {
                Results = new List<SearchConsoleResult>()
            };

            var sqlQuery =
                "SELECT DataId, Keyword, PageUrl, Impressions, Clicks, CTR, Position, Category, SubCategory1, SubCategory2, Intent, Colour FROM AnalysisData WHERE Id = @Id";
            using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var searchResult = new SearchConsoleResult();
                searchResult.DataId = (int) reader["DataId"];
                searchResult.Keyword = reader["Keyword"].ToString();
                searchResult.Impressions = (int) reader["Impressions"];
                searchResult.Clicks = (int) reader["Clicks"];
                searchResult.Position = (int) reader["Position"];
                searchResult.Category = reader["Category"].ToString();
                searchResult.SubCategory1 = reader["SubCategory1"].ToString();
                searchResult.SubCategory2 = reader["SubCategory2"].ToString();
                searchResult.Intent = reader["Intent"].ToString();
                searchResult.Colour = reader["Colour"].ToString();
                searchResult.Page = reader["PageUrl"].ToString();
                searchResult.Ctr = Double.Parse(reader["CTR"].ToString());
                dataResult.Results.Add(searchResult);
            }

            conn.Close();

            return dataResult;
        }

        private SearchConsoleData GetSearchConsoleData(RequestObject request)
        {
            var dataResult = new SearchConsoleData
            {
                Results = new List<SearchConsoleResult>()
            };

            using var searchConsole = new Google.Apis.Webmasters.v3.WebmastersService(
                new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Search Console API test"
                });
            SearchAnalyticsQueryRequest body = new SearchAnalyticsQueryRequest();
            IList<string> dimensions = new List<string>();
            dimensions.Add("query");
            dimensions.Add("page");
            body.StartDate = DateTime.Today.AddDays(-90).ToShortDateString();
            body.EndDate = DateTime.Today.ToShortDateString();
            body.Dimensions = dimensions;
            body.RowLimit = 25000;

            var result = searchConsole.Searchanalytics.Query(body, request.siteUrl).Execute();

            List<SearchConsoleResult> dataList = new List<SearchConsoleResult>();

            foreach (var row in result.Rows)
            {
                dataList.Add(new SearchConsoleResult
                {
                    Keyword = row.Keys[0],
                    Page = row.Keys[1],
                    Clicks = (int) row.Clicks,
                    Impressions = (int) row.Impressions,
                    Ctr = row.Ctr,
                    Position = (int) row.Position
                });
            }

            dataResult.Results.AddRange(dataList);
            dataResult.NumberOfRows = dataList.Count;

            return dataResult;
        }
    }
}