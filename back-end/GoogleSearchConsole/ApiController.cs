using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Webmasters.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace back_end.GoogleSearchConsole
{
    public class ApiController
    {

        private readonly ConnectionStrings connString;

        public ApiController(IOptions<ConnectionStrings> connStringsAccessor)
        {
            connString = connStringsAccessor.Value;
        }

        private static readonly GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential
            .FromFile("credentials.json")
            .CreateScoped(new[]
            {
                Google.Apis.Webmasters.v3.WebmastersService.Scope.WebmastersReadonly
            });

        public static SearchConsoleData GetData(RequestObject request)
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

                var dataResult = new SearchConsoleData
                {
                    Results = new List<SearchConsoleResult>()
                };
                dataResult.Results.AddRange(dataList);
                dataResult.NumberOfRows = dataList.Count;

                return dataResult;
            }
        }
    }
}