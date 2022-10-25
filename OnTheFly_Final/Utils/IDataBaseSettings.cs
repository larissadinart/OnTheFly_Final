namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
        string CompanyCollectionName { get; set; }
        string GarbageCollectionName { get; set; }
        string BlockedCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
