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

        [Route("/siteUrl")]
        [HttpGet]
        public string GetData([FromBody] RequestObject request)
        {
            using (var searchConsole = new Google.Apis.Webmasters.v3.WebmastersService(
                new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Search Console API test"
                }))
            {
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
                        Query = row.Keys[0],
                        Page = row.Keys[1],
                        Clicks = (int)row.Clicks,
                        Impressions = (int)row.Impressions,
                        Ctr = row.Ctr,
                        Position = (int)row.Position
                    });
                }

                var dataResult = new SearchConsoleData
                {
                    Results = new List<SearchConsoleResult>()
                };
                dataResult.Results.AddRange(dataList);
                dataResult.NumberOfRows = dataList.Count;

                return JsonConvert.SerializeObject(dataResult);
            }
        }


        [Route("/CreateAnalysis")]
        [HttpPost("{id}")]
        public string CreateAnalysis([FromBody] RequestObject request)
        {
            var dataResult = new SearchConsoleData
            {
                Results = new List<SearchConsoleResult>()
            };
            using (var searchConsole = new Google.Apis.Webmasters.v3.WebmastersService(
                new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Search Console API test"
                }))
            {
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
                        Query = row.Keys[0],
                        Page = row.Keys[1],
                        Clicks = (int)row.Clicks,
                        Impressions = (int) row.Impressions,
                        Ctr = row.Ctr,
                        Position = (int)row.Position
                    });
                }

                dataResult.Results.AddRange(dataList);
                dataResult.NumberOfRows = dataList.Count;
            }


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

                foreach (var data in dataResult.Results)
                {
                    var row = table.NewRow();
                    row["Keyword"] = data.Query;
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


                var dataResult2 = new SearchConsoleData();
                dataResult2.Results = new List<SearchConsoleResult>();

                var sqlQuery2 =
                    "SELECT DataId, Keyword, PageUrl, Impressions, Clicks, CTR, Position, Category, SubCategory1, SubCategory2, Intent FROM AnalysisData WHERE Id = @Id";
                using SqlCommand cmd2 = new SqlCommand(sqlQuery2, conn);
                cmd2.Parameters.Add("@Id", SqlDbType.Int).Value = newId;

                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    var searchResult = new SearchConsoleResult();
                    searchResult.DataId = (int) reader2["DataId"];
                    searchResult.Query = reader2["Keyword"].ToString();
                    searchResult.Impressions = (int) reader2["Impressions"];
                    searchResult.Clicks = (int) reader2["Clicks"];
                    searchResult.Position = (int) reader2["Position"];
                    searchResult.Category = reader2["Category"].ToString();
                    searchResult.SubCategory1 = reader2["SubCategory1"].ToString();
                    searchResult.SubCategory2 = reader2["SubCategory2"].ToString();
                    searchResult.Intent = reader2["Intent"].ToString();
                    searchResult.Page = reader2["PageUrl"].ToString();
                    searchResult.Ctr = Double.Parse(reader2["CTR"].ToString());
                    dataResult2.Results.Add(searchResult);
                }

                conn.Close();

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
            var dataResult = new SearchConsoleData();
            dataResult.Results = new List<SearchConsoleResult>();

            try
            {
                using SqlConnection conn = new SqlConnection(connString.SQLConnection);

                var sqlQuery1 = "SELECT TOP 1 Id, Customer, Url, CreatedDate FROM Analyses WHERE Id = @Id";

                conn.Open();

                using SqlCommand cmd1 = new SqlCommand(sqlQuery1, conn);
                cmd1.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                var reader = cmd1.ExecuteReader();

                while (reader.Read())
                {
                    dataResult.CustomerName = (string) reader["Customer"];
                    dataResult.SiteUrl = (string) reader["Url"];
                }

                reader.Close();

                var sqlQuery2 =
                    "SELECT DataId, Keyword, PageUrl, Impressions, Clicks, CTR, Position, Category, SubCategory1, SubCategory2, Intent FROM AnalysisData WHERE Id = @Id";
                using SqlCommand cmd2 = new SqlCommand(sqlQuery2, conn);
                cmd2.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    var searchResult = new SearchConsoleResult();
                    searchResult.DataId = (int) reader2["DataId"];
                    searchResult.Query = reader2["Keyword"].ToString();
                    searchResult.Impressions = (int) reader2["Impressions"];
                    searchResult.Clicks = (int) reader2["Clicks"];
                    searchResult.Position = (int) reader2["Position"];
                    searchResult.Category = reader2["Category"].ToString();
                    searchResult.SubCategory1 = reader2["SubCategory1"].ToString();
                    searchResult.SubCategory2 = reader2["SubCategory2"].ToString();
                    searchResult.Intent = reader2["Intent"].ToString();
                    searchResult.Page = reader2["PageUrl"].ToString();
                    searchResult.Ctr = Double.Parse(reader2["CTR"].ToString());
                    dataResult.Results.Add(searchResult);
                }

                conn.Close();

                dataResult.NumberOfRows = dataResult.Results.Count;
                dataResult.AnalysisId = id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return JsonConvert.SerializeObject(dataResult);
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
                    "SELECT a.Id, Customer, Url, CreatedDate FROM Analyses A INNER JOIN AnalysisData ad ON a.Id = ad.Id " +
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

        private class AnalysisMeta
        {
            public string id { get; set; }
            public string customerName { get; set; }
            public string url { get; set; }
            public DateTime createdDate { get; set; }
        }


        private static readonly GoogleCredential credential = GoogleCredential
            .FromFile("credentials.json")
            .CreateScoped(new[]
            {
                Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });
    }
}