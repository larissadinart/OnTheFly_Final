namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
        string FlightCollectionName { get; set; }
        string SalesCollectionName { get; set; }
        string AircraftCollectionName { get; set; }
        string PassengerCollectionName { get; set; }
        string PassengerCollectionGarbage { get; set; }
        string PassengerCollectionRestricted { get; set; }
        string AircraftGarbageCollectionName { get; set; }
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string AircraftDatabaseName { get; set; }
        string PassengerDataBaseName { get; set; }
        string FlightsDataBaseName { get; set; }
        string SalesDataBaseName { get; set; }
        string AirportDataBaseName { get; set; }
    }
}
