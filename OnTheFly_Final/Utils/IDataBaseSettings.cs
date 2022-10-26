namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
         string SalesCollectionName { get; set; }
         string PassengerCollectionName { get; set; }
         string PassengerGarbageCollectionName { get; set; }
         string PassengerRestrictedCollectionName { get; set; }
         string AddressCollectionName { get; set; }
         string ConnectionString { get; set; }
         string PassengerDataBaseName { get; set; }

    }
}
