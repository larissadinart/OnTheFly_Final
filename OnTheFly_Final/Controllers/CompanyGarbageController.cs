using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGarbageController : ControllerBase
    {
        private readonly CompanyGarbageServices _companyGarbageServices;
        private readonly CompanyServices _companyServices;

        public CompanyGarbageController(CompanyGarbageServices companyGarbageServices, CompanyServices companyServices)
        {
            _companyGarbageServices = companyGarbageServices;
            _companyServices = companyServices;
        }

        [HttpPost("{cnpj:length(19)}")]
        public ActionResult<CompanyGarbage> PostCompany(CompanyGarbage companyGarbage, string cnpj)
        {
            Company companyIn = _companyServices.GetCompany(cnpj);


            companyGarbage.CNPJ = companyIn.CNPJ;
            companyGarbage.Name = companyIn.Name;
            companyGarbage.NameOpt = companyIn.NameOpt;
            companyGarbage.DtOpen = companyIn.DtOpen;
            companyGarbage.Status = companyIn.Status;

            _companyGarbageServices.CreateCompanyGarbage(companyGarbage);
            return CreatedAtRoute("GetCompanyGarbage", new { cnpj = companyIn.CNPJ.ToString() }, companyIn);
        }

        [HttpGet]
        public ActionResult<List<CompanyGarbage>> GetAllCompanyGarbage() => _companyGarbageServices.GetAllCompanyGarbage();


        [HttpGet("{cnpj}", Name = "GetCompanyGarbage")]
        public ActionResult<Company> GetCompanyGarbage(string cnpj)
        {
            var company = _companyGarbageServices.GetCompanyGarbage(cnpj);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

    }
}
