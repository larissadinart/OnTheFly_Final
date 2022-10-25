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
        public CompanyControllers(CompanyServices companyServices)
        {
            _companyServices = companyServices;
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAllCompany() => _companyServices.GetAllCompany();

        [HttpGet("{cnpj}", Name = "GetOneCompany")]
        public ActionResult<Company> GetOneCompany(string cnpj)
        {
            var company = _companyServices.GetOneCompany(cnpj);
            if (company == null) 
                return NotFound("Something went wrong in the request, company not found!");

            return Ok(company);

        }
        [HttpPost]
        public ActionResult<Company> CreateCompany(Company company)
        {
            _companyServices.CreateCompany(company);
            return CreatedAtRoute("GetOneCompany", new { cnpj = company.CNPJ.ToString() }, company);

        }

        [HttpPut]
        public ActionResult<Company> PutCompany(Company companyIn, string cnpj)
        {
            var company = _companyServices.GetOneCompany(cnpj);
            if (company == null) return NotFound("Something went wrong in the request, company not found!");

            companyIn.CNPJ = cnpj;
            _companyServices.UpdateCompany(companyIn.CNPJ, companyIn);
            return NotFound("Something went wrong in the request, company not found!");

        }
        [HttpDelete]
        public ActionResult<Company> DeleteCompany(string cnpj)
        {
            //procurar se existe aquela companhia cadastrada para poder cadastrar atraves do cnpj
            Company company = _companyServices.GetOneCompany(cnpj);
            if (company == null) return NotFound("Something went wrong in the request, company not found!");
            //antes de remover, add na collection Garbage
            _companyServices.RemoveCompany(company);
            return NoContent();
        }

    }
}

