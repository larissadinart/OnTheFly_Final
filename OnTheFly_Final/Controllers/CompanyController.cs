using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using OnTheFly_Final.Utils;
using System;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        readonly CompanyUtils companyUtils = new();
        private readonly CompanyServices _companyServices;
        private readonly CompanyGarbageServices _companyGarbageServices;
        private readonly CompanyBlockedServices _companyBlockedServices;
        private readonly AddressServices _addressServices;
        public CompanyController(CompanyServices companyServices, CompanyGarbageServices companyGarbageServices, CompanyBlockedServices companyBlockedServices, AddressServices addressServices)
        {
            _companyServices = companyServices;
            _companyGarbageServices = companyGarbageServices;
            _companyBlockedServices = companyBlockedServices;
            _addressServices = addressServices;
        }

        [HttpPost]
        public ActionResult<Company> CreateCompany(Company company)
        {
            var cep = company.Address.ZipCode;
            var address = _addressServices.GetAdress(cep).Result;
            if (address == null)
                return NotFound("Endereço não encontrado!");
            else
            {
                address.Number = company.Address.Number;
                address.Complement = company.Address.Complement;
                company.Address = address;
            }
            if (companyUtils.IsCnpjValid(company.CNPJ) == false)
            {
                return BadRequest("CNPJ inválido!");

            }
            else
            {
                var Cnpj = company.CNPJ;
                //var Cnpj = companyUtils.FormatCNPJ(company.CNPJ);
                company.CNPJ = Cnpj.Substring(0, 2).ToString() + "." + Cnpj.Substring(2, 3).ToString() + "." + Cnpj.Substring(5, 3).ToString() + '/' + Cnpj.Substring(8, 4).ToString() + "-" + Cnpj.Substring(12, 2).ToString();
                
                var comp = _companyServices.GetCompany(company.CNPJ);
                if (comp != null) return BadRequest("Companhia já cadastrada com esse CNPJ!");
                if (company.NameOpt == null)
                {
                    company.NameOpt = company.Name;
                }

                DateTime date = company.DtOpen;
                date = DateTime.Parse(date.ToShortDateString());
                if (date > DateTime.Now)
                {
                    return BadRequest("A data de abertura não pode ser maior que a data atual!");
                }

                TimeSpan result;
                result = DateTime.Now - date;

                if (result.Days / 30 < 6)
                {
                    return BadRequest($"A companhia tem {result.Days / 30} meses, o tempo é insufiente para finalizar o cadastro!");
                }

                _companyServices.CreateCompany(company);
                return CreatedAtRoute("GetCompany", new { cnpj = company.CNPJ.ToString() }, company);
            }
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAllCompany() => _companyServices.GetAllCompany();


        [HttpGet("{cnpj}", Name = "GetCompany")]
        public ActionResult<Company> GetCompany(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace("%2F", "/");

            var company = _companyServices.GetCompany(cnpj);

            if (company == null)
                return NotFound("Something went wrong in the request, company not found!");

            return Ok(company);
        }


        [HttpPut]
        public ActionResult<Company> PutCompany(string cnpj, string nameOpt, bool status)
        {
            Company companyIn = new() { CNPJ = cnpj, NameOpt = nameOpt, Status = status};
            if (companyUtils.IsCnpjValid(companyIn.CNPJ) == false)
                return BadRequest("CNPJ inválido!");

            var company = _companyServices.GetCompany(cnpj);
            if (company == null) return NotFound("Something went wrong in the request, company not found!");

            companyIn.CNPJ = cnpj;
            _companyServices.UpdateCompany(companyIn.CNPJ, companyIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Company> DeleteCompany(string cnpj)
        {
            var company = _companyServices.GetCompany(cnpj);
            if (company == null)
                return NotFound("Something went wrong in the request, company not found!");

            CompanyGarbage companyGarbage = new()
            {
                CNPJ = company.CNPJ,
                Name = company.Name,
                NameOpt = company.NameOpt,
                DtOpen = company.DtOpen,
                Status = company.Status
            };

            _companyServices.RemoveCompany(company);

            _companyGarbageServices.CreateCompanyGarbage(companyGarbage);

            return NoContent();
        }
    }
}

