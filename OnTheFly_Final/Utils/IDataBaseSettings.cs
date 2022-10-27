namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
        string AddressCollectionName { get; set; }
        string CompanyCollectionName { get; set; }
        string CompanyGarbageCollectionName { get; set; }
        string CompanyBlockedCollectionName { get; set; }
        string ConnectionString { get; set; }
        string CompanyDatabaseName { get; set; }    
    }
}
