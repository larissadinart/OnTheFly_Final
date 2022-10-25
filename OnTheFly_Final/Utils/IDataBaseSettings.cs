namespace OnTheFly_Final.Utils
{
    public interface IDataBaseSettings
    {
        string CompanyCollectionName { get; set; }
        string CompanyCollectionGarbage { get; set; }
        string CompanyCollectionBlocked { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
