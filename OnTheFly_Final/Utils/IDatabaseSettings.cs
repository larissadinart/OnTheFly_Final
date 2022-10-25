namespace OnTheFly_Final.Utils
{
    public interface IDatabaseSettings
    {
        string SalesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
