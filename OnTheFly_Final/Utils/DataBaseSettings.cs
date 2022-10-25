namespace OnTheFly_Final.Utils
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string CompanyCollectionName { get; set; }
        public string GarbageCollectionName { get; set; }
        public string BlockedCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
