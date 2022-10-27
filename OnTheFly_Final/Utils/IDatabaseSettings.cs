namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
        string ConnectionString { get; set; }
        string AircraftDataBaseName { get; set; }
        string AircraftCollectionName { get; set; }
        string AircraftGarbageCollectionName { get; set; }
        string CompanyDatabaseName { get; set; }
        string CompanyCollectionName { get; set; }
    }
}
