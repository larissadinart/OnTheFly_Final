using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;
using System.Collections.Generic;

namespace OnTheFly_Final.Services
{
    public class CompanyServices
    {
        private readonly IMongoCollection<Company> _company;
        public CompanyServices(IDataBaseSettings settings)
        {
            var company = new MongoClient(settings.ConnectionString);
            var database = company.GetDatabase(settings.CompanyDatabaseName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);

        }

        public Company CreateCompany(Company company)
        {
            _company.InsertOne(company);
            return company;
        }

        public List<Company> GetAllCompany() => _company.Find<Company>(company => true).ToList();

        public Company GetCompany(string cnpj) => _company.Find<Company>(company => company.CNPJ == cnpj).FirstOrDefault();
        public void UpdateCompany(string cnpj, Company companyIn)
        {
            _company.ReplaceOne(company => company.CNPJ == cnpj, companyIn);
        }
        public void RemoveCompany(Company companyIn) => _company.DeleteOne(company => company.CNPJ == companyIn.CNPJ);
    }
}
