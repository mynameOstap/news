namespace Model


{
    public class NewsResponse
    {
        public List<sources> sources { get; set; }
    }

    public class sources
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string country { get; set; }
    }
}