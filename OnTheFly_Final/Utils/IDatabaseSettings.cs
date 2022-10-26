namespace OnTheFly_Final.Utils
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseAircraft { get; set; }
        string AircraftCollectionName { get; set; }
        string AircraftGarbageCollectionName { get; set; }
    }
}
