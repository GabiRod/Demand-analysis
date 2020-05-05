namespace back_end.GoogleSearchConsole
{
    public class RequestObject
    {
        public string customerName { get; set; }
        public string siteUrl { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int rowLimit { get; set; }
    }
}