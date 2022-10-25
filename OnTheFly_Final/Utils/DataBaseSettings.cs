namespace OnTheFly_Final.Utils
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string CompanyCollectionName { get; set; }
        public string CompanyCollectionGarbage { get; set; }
        public string CompanyCollectionBlocked { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
