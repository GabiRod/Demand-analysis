using System.Collections.Generic;

namespace back_end.GoogleSearchConsole
{
    public class Data
    {
        public string Funnel { get; set; }
        public string Intent { get; set; }
        public string Category { get; set; }
        public List<string> Subcategories { get; set; }
        public string Keyword { get; set; }
        public double Volume { get; set; }
        public double Position { get; set; }
        public double CTA { get; set; }
        public string URL { get; set; }

    }
}