namespace OnTheFly_Final.Utils
{
    public class DataBaseSettings:IDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string AircraftDataBaseName { get; set; }
        public string AircraftCollectionName { get; set; }
        public string AircraftGarbageCollectionName { get; set; }
        public string CompanyDatabaseName { get; set; }

        public string CompanyCollectionName { get; set; }
    }
}
