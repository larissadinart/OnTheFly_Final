namespace OnTheFly_Final.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string SalesCollectionName { get; set; }
    }
}
