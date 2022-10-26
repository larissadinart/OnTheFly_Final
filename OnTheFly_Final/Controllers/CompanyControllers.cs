using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyControllers : ControllerBase
    {
        private readonly CompanyServices _companyServices;
        private readonly CompanyGarbageServices _companyGarbageServices;
        public CompanyControllers(CompanyServices companyServices, CompanyGarbageServices companyGarbageServices)
        {
            _companyServices = companyServices;
            _companyGarbageServices = companyGarbageServices;
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAllCompany() => _companyServices.GetAllCompany();


        [HttpGet("{cnpj}", Name = "GetCompany")]
        public ActionResult<Company> GetCompany(string cnpj)
        {
            var company = _companyServices.GetCompany(cnpj);

            if (company == null) 
                return NotFound("Something went wrong in the request, company not found!");

            return Ok(company);

        }
        [HttpPost("{cnpj:length(19)}")]
        public ActionResult<Company> CreateCompany(Company company)
        {
            _companyServices.CreateCompany(company);
            return CreatedAtRoute("GetOneCompany", new { cnpj = company.CNPJ.ToString() }, company);

        }

        [HttpPut]
        public ActionResult<Company> PutCompany(Company companyIn, string cnpj)
        {
            var company = _companyServices.GetCompany(cnpj);
            if (company == null) return NotFound("Something went wrong in the request, company not found!");

            companyIn.CNPJ = cnpj;
            _companyServices.UpdateCompany(companyIn.CNPJ, companyIn);
            return NotFound("Something went wrong in the request, company not found!");

        }
        [HttpDelete]
        public ActionResult<Company> DeleteCompany(string cnpj)
        {
            var company = _companyServices.GetCompany(cnpj);
            if (company == null) 
                return NotFound("Something went wrong in the request, company not found!");

            CompanyGarbage companyGarbage = new CompanyGarbage();

            companyGarbage.CNPJ = company.CNPJ;
            companyGarbage.Name = company.Name;
            companyGarbage.NameOpt = company.NameOpt;
            companyGarbage.DtOpen = company.DtOpen;
            companyGarbage.Status = company.Status;

            _companyServices.RemoveCompany(company);

            _companyGarbageServices.CreateCompanyGarbage(companyGarbage);

            return NoContent();
        }

    }
}

