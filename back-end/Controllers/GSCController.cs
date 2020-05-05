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
                body.StartDate = request.startDate;
                body.EndDate = request.endDate;
                body.Dimensions = dimensions;
                body.RowLimit = request.rowLimit > 250000 ? 25000 : request.rowLimit;

                var result = searchConsole.Searchanalytics.Query(body, request.siteUrl).Execute();

                List<SearchConsoleResult> dataList = new List<SearchConsoleResult>();

                foreach (var row in result.Rows)
                {
                    dataList.Add(new SearchConsoleResult
                    {
                        Query = row.Keys[0],
                        Page = row.Keys[1],
                        Clicks = row.Clicks,
                        Impressions = row.Impressions,
                        Ctr = row.Ctr,
                        Position = row.Position
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

        // POST: api/GSC
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GSC/5
        [Route("/CreateAnalysis")]
        [HttpPut("{id}")]
        public string PutCreateAnalysis([FromBody] RequestObject request)
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
                body.StartDate = request.startDate;
                body.EndDate = request.endDate;
                body.Dimensions = dimensions;
                body.RowLimit = request.rowLimit;

                var result = searchConsole.Searchanalytics.Query(body, request.siteUrl).Execute();

                List<SearchConsoleResult> dataList = new List<SearchConsoleResult>();

                foreach (var row in result.Rows)
                {
                    dataList.Add(new SearchConsoleResult
                    {
                        Query = row.Keys[0],
                        Page = row.Keys[1],
                        Clicks = row.Clicks,
                        Impressions = row.Impressions,
                        Ctr = row.Ctr,
                        Position = row.Position
                    });
                }

                dataResult.Results.AddRange(dataList);
                dataResult.NumberOfRows = dataList.Count;
            }

            var conString = connString.SQLConnection;

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
                table.Columns.Add("Keyword", typeof(string));
                table.Columns.Add("PageUrl", typeof(string));
                table.Columns.Add("Impressions", typeof(int));
                table.Columns.Add("CTR", typeof(double));
                table.Columns.Add("Position", typeof(int));
                table.Columns.Add("Category", typeof(int));
                table.Columns.Add("SubCategory1", typeof(string));
                table.Columns.Add("SubCategory2", typeof(string));
                table.Columns.Add("Intent", typeof(string));

                foreach (var data in dataResult.Results)
                {
                    var row = table.NewRow();
                    row["Keyword"] = data.Query;
                    row["PageUrl"] = data.Page;
                    row["Impressions"] = data.Impressions;
                    row["CTR"] = data.Ctr;
                    row["Position"] = data.Position;

                    table.Rows.Add(row);
                }

                using (var bulk = new SqlBulkCopy(conn))
                {
                    bulk.DestinationTableName = "AnalysisData";
                    bulk.WriteToServer(table);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return "test";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private static readonly GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential
            .FromFile("credentials.json")
            .CreateScoped(new[]
            {
                Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });
    }
}