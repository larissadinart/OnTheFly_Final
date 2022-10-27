using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;
using System.Collections.Generic;

namespace OnTheFly_Final.Services
{
    public class CompanyBlockedServices
    {
        private readonly IMongoCollection<CompanyBlocked> _companyBlocked;

        public CompanyBlockedServices(IDataBaseSettings settings)
        {
            var companyBlocked = new MongoClient(settings.ConnectionString);
            var database = companyBlocked.GetDatabase(settings.CompanyDatabaseName);
            _companyBlocked = database.GetCollection<CompanyBlocked>(settings.CompanyBlockedCollectionName);
        }
        public CompanyBlocked CreateCompanyBlocked(CompanyBlocked companyBlocked)
        {
            _companyBlocked.InsertOne(companyBlocked);
            return companyBlocked;
        }
        public List<CompanyBlocked> GetAllCompanyBlocked() => _companyBlocked.Find<CompanyBlocked>(companyBlocked => true).ToList();

        public CompanyBlocked GetCompanyBlocked(string cnpj) => _companyBlocked.Find<CompanyBlocked>(companyBlocked => companyBlocked.CNPJ == cnpj).FirstOrDefault();
        public void RemoveCompanyBlocked(CompanyBlocked companyIn) => _companyBlocked.DeleteOne(companyBlocked => companyBlocked.CNPJ == companyIn.CNPJ);
    }
}