using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGarbageControllers : ControllerBase
    {
        private readonly CompanyGarbageServices _companyGarbageServices;
        private readonly CompanyServices _companyServices;

        public CompanyGarbageControllers(CompanyGarbageServices companyGarbageServices, CompanyServices companyServices)
        {
            _companyGarbageServices = companyGarbageServices;
            _companyServices = companyServices;
        }

        [HttpPost]
        public ActionResult<CompanyControllers> PostCompany(CompanyGarbage companyGarbage, string cnpj)
        {
            Company companyIn = _companyServices.GetOneCompany(cnpj);
            companyGarbage.Company.CNPJ = companyIn.CNPJ;
            companyGarbage.Company.Name = companyIn.Name;
            companyGarbage.Company.NameOpt = companyIn.NameOpt;
            companyGarbage.Company.DtOpen = companyIn.DtOpen;
            companyGarbage.Company.Status = companyIn.Status;

            _companyGarbageServices.CreateCompanyGarbage(companyGarbage);
            return CreatedAtRoute("GetCompanyGarbage", new { cnpj = companyIn.CNPJ.ToString() }, companyIn);
        }

        [HttpGet("{cnpj:length(19)}", Name = "GetCompanyGarbage")]
        public ActionResult<Company> GetOneCompany(string cnpj)
        {
            var company = _companyServices.GetOneCompany(cnpj);
            if (company == null)
                return NotFound("Something went wrong in the request, company not found!");

            return Ok(company);
        }
    }
}
