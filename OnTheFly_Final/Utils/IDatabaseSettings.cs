﻿namespace OnTheFly_Final.Utils
{
    public class IDataBaseSettings
    {
        string SalesCollectionName { get; set; }
        public string PassengerCollectionName { get; set; }
        public string PassengerCollectionGarbage { get; set; }
        public string PassengerCollectionRestricted { get; set; }
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }
}
