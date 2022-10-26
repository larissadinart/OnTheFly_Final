namespace OnTheFly_Final.Utils
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AircraftCollectionName { get; set; }
    }
}
