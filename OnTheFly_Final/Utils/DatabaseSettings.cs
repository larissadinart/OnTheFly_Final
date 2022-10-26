namespace OnTheFly_Final.Utils
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string FlightCollectionName { get; set; }
        public string SalesCollectionName { get; set; }
        public string PassengerCollectionName { get; set; }
        public string AircraftCollectionName { get; set; }
        public string PassengerCollectionGarbage { get; set; }
        public string PassengerCollectionRestricted { get; set; }
        public string AirportCollectionName { get; set; }   
        public string ConnectionString { get; set; }
        public string AircraftDatabaseName { get; set; }
        public string PassengerDataBaseName { get; set; }
        public string FlightsDataBaseName { get; set; }
        public string SalesDataBaseName { get; set; }
        public string AirportDataBaseName { get; set; }
    }
}
