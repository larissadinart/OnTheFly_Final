using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyServices _companyServices;
        private readonly CompanyGarbageServices _companyGarbageServices;
        private readonly CompanyBlockedServices _companyBlockedServices;

        public CompanyController(CompanyServices companyServices, CompanyGarbageServices companyGarbageServices, CompanyBlockedServices companyBlockedServices)
        {
            _companyServices = companyServices;
            _companyGarbageServices = companyGarbageServices;
            _companyBlockedServices = companyBlockedServices;
        }

        [HttpPost]
        public ActionResult<Company> CreateCompany(Company company)
        {
            string cnpj = company.CNPJ;
            if(cnpj.Length < 14)
            {
                return NotFound("CNPJ inválido!");
            }
            company.CNPJ = cnpj.Substring(0, 2).ToString() + "." + cnpj.Substring(2, 3).ToString() + "." + cnpj.Substring(5, 3).ToString() + "/" + cnpj.Substring(8, 4).ToString() + "-" + cnpj.Substring(12, 2).ToString();
            DateTime date = company.DtOpen;
            date = DateTime.Parse(date.ToShortDateString());
            if(company.NameOpt == null)
            {
                company.NameOpt = company.Name;
            }
            _companyServices.CreateCompany(company);
            return CreatedAtRoute("GetCompany", new { cnpj = company.CNPJ.ToString() }, company);
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

