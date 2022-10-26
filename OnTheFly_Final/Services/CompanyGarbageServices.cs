using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class CompanyGarbageServices
    {
        private readonly IMongoCollection<CompanyGarbage> _companyGarbage;

        public CompanyGarbageServices(IDataBaseSettings settings)
        {
            var companyGarbage = new MongoClient(settings.ConnectionString);
            var database = companyGarbage.GetDatabase(settings.CompanyDatabaseName);
            _companyGarbage = database.GetCollection<CompanyGarbage>(settings.CompanyGarbageCollectionName);

        }

        public CompanyGarbage CreateCompanyGarbage(CompanyGarbage companyGarbage)
        {
            _companyGarbage.InsertOne(companyGarbage);
            return companyGarbage;
        }
        public CompanyGarbage GetCompanyGarbage(string cnpj) => _companyGarbage.Find<CompanyGarbage>(companyGarbage => companyGarbage.Company.CNPJ == cnpj).FirstOrDefault();
    }
}
