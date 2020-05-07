using System.Collections.Generic;

namespace back_end.GoogleSearchConsole
{
    public class SearchConsoleData
    {
        public int NumberOfRows { get; set; }
        public int AnalysisId { get; set; }
        public string CustomerName { get; set; }
        public string SiteUrl { get; set; }
        public List<SearchConsoleResult> Results { get; set; }
    }

    public class SearchConsoleResult
    {
        public int DataId { get; set; }
        public string Query { get; set; }
        public string Page { get; set; }
        public int Clicks { get; set; }
        public int Impressions { get; set; }
        public double? Ctr { get; set; }
        public int Position { get; set; }
        public string? Category { get; set; }
        public string? SubCategory1 { get; set; }
        public string? SubCategory2 { get; set; }
        public string Intent { get; set; }
    }
}