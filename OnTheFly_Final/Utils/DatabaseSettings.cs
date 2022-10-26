namespace OnTheFly_Final.Utils
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string AircraftDatabaseName { get; set; }
        public string AircraftCollectionName { get; set; }
        public string AircraftGarbageCollectionName { get; set; }
    }
}
