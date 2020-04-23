using System.Collections.Generic;

namespace back_end.GoogleSearchConsole
{
    public class SearchConsoleData
    {
        public int NumberOfRows { get; set; }
        public List<SearchConsoleResult> Results { get; set; }
    }

    public class SearchConsoleResult
    {
        public string Query { get; set; }
        public string Page { get; set; }
        public double? Clicks { get; set; }
        public double? Impressions { get; set; }
        public double? Ctr { get; set; }
        public double? Position { get; set; }
    }
}