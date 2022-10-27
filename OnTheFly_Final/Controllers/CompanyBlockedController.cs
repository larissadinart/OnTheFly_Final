using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyBlockedController : ControllerBase
    {
        private readonly CompanyBlockedServices _companyBlockedServices;
        private readonly CompanyServices _companyServices;

        public CompanyBlockedController(CompanyBlockedServices companyBlockedServices, CompanyServices companyServices)
        {
            _companyBlockedServices = companyBlockedServices;
            _companyServices = companyServices;
        }

        [HttpGet]
        public ActionResult<List<CompanyBlocked>> GetAllCompanyBlocked() => _companyBlockedServices.GetAllCompanyBlocked();


        [HttpGet("{cnpj}", Name = "GetCompanyBlocked")]
        public ActionResult<CompanyBlocked> GetCompanyBlocked(string cnpj)
        {
            var companyBlocked = _companyBlockedServices.GetCompanyBlocked(cnpj);

            if (companyBlocked == null)
                return NotFound();

            return Ok(companyBlocked);
        }

        [HttpPost("{cnpj}")]
        public ActionResult<CompanyBlocked> PostCompanyBlocked(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", ".").Replace("-", "-").Replace("%", "/").Replace("F", "");

            var company = _companyServices.GetCompany(cnpj);
            if (company == null) return NotFound("Companhia não encontrada!!");
            CompanyBlocked companyBlocked = new CompanyBlocked() { CNPJ = company.CNPJ, Name = company.Name, NameOpt = company.NameOpt, DtOpen = company.DtOpen, Status = company.Status };

            _companyBlockedServices.CreateCompanyBlocked(companyBlocked);
            company.Status = false;
            _companyServices.UpdateCompany(cnpj, company);
            return Ok(companyBlocked);
        }
       
    }
}
