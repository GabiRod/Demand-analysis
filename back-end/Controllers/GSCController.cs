using System;
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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace back_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GSCController : ControllerBase
    {
        private readonly ConnectionStrings connString;

        public GSCController(IOptions<ConnectionStrings> connStringsAccessor)
        {
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

                var sqlQuery = "INSERT INTO Analyses (Customer, Url, CreatedDate) output INSERTED.Id " +
                               "VALUES (@Customer, @Url, @CreatedDate) ";

                conn.Open();

                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.Add("@Customer", SqlDbType.NVarChar).Value = request.customerName;
                cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = request.siteUrl;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = DateTime.UtcNow;

                var newId = (int) cmd.ExecuteScalar();

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

                List<string> keywords = new List<string>();

                foreach (var data in dataResult.Results)
                {
                    var row = table.NewRow();
                    row["Keyword"] = data.Keyword;
                    row["SiteUrl"] = data.Page;
                    row["Clicks"] = data.Clicks;
                    row["Impressions"] = data.Impressions;
                    row["CTR"] = data.Ctr;
                    row["Position"] = data.Position;


                    table.Rows.Add(row);
                }

                using var bulk = new SqlBulkCopy(conn)
                {
                    DestinationTableName = "AnalysisData"
                };

                bulk.WriteToServer(table);
                
                
                
                conn.Close();
                
                var dataResult2 = GetAnalysis(conn, newId);

                dataResult2.NumberOfRows = dataResult2.Results.Count;
                dataResult2.AnalysisId = newId;
                dataResult2.CustomerName = request.customerName;
                dataResult2.SiteUrl = request.siteUrl;


                return JsonConvert.SerializeObject(dataResult2);
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

                var dataResult = GetAnalysis(conn, id);

                var sqlQuery1 = "SELECT TOP 1 Id, Customer, Url, CreatedDate FROM Analyses WHERE Id = @Id";

                using SqlCommand cmd = new SqlCommand(sqlQuery1, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataResult.CustomerName = (string) reader["Customer"];
                    dataResult.SiteUrl = (string) reader["Url"];
                }

                reader.Close();
                conn.Close();

                dataResult.NumberOfRows = dataResult.Results.Count;
                dataResult.AnalysisId = id;

                return JsonConvert.SerializeObject(dataResult);
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
                                "SubCategory1 = @SubCategory1," +
                                "SubCategory2 = @SubCategory2," +
                                "Intent = @Intent " +
                                "WHERE DataId = @DataId";

                using SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = categoryData.category;
                cmd.Parameters.Add("@SubCategory1", SqlDbType.NVarChar).Value = categoryData.subCategory1;
                cmd.Parameters.Add("@SubCategory2", SqlDbType.NVarChar).Value = categoryData.subCategory2;
                cmd.Parameters.Add("@Intent", SqlDbType.NVarChar).Value = categoryData.intent;
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

        //[Route("/analysis/{id}/keywords")]
        //[HttpGet]
        //public string GetSplitKeywords(int id)
        //{

        //}

        public class CategoryData
        {
            public string? category { get; set; }
            public string? subCategory1 { get; set; }
            public string? subCategory2 { get; set; }
            public string? intent { get; set; }
        }

        private class AnalysisMeta
        {
            public int numberOfRows { get; set; }
            public string id { get; set; }
            public string customerName { get; set; }
            public string url { get; set; }
            public DateTime createdDate { get; set; }
        }

        private SearchConsoleData GetAnalysis(SqlConnection conn, int id)
        {
            var dataResult = new SearchConsoleData
            {
                Results = new List<SearchConsoleResult>()
            };

            var sqlQuery =
                "SELECT DataId, Keyword, PageUrl, Impressions, Clicks, CTR, Position, Category, SubCategory1, SubCategory2, Intent FROM AnalysisData WHERE Id = @Id";
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

        private static readonly GoogleCredential credential = GoogleCredential
            .FromFile("credentials.json")
            .CreateScoped(new[]
            {
                Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });
    }
}