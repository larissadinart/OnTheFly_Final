using OnTheFly_Final.Utils.Data;

namespace OnTheFly_Final.Utils
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string SalesCollectionName { get; set; }
        public string PassengerCollectionName { get; set; }
        public string PassengerGarbageCollectionName { get; set; }
        public string PassengerRestrictedCollectionName { get; set; }
        public string AddressCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string PassengerDataBaseName { get; set; }

    }
}
