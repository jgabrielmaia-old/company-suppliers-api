using Microsoft.AspNetCore.Mvc;
using CompanySupplier.Entity.Model;
using CompanySupplier.WebApi.Repositories;
using CompanySupplier.WebApi.Model;

namespace CompanySupplier.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet("{id}", Name = "GetCompany")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyRepository.GetCompany(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet]
        public ActionResult<Company> Get()
        {
            var companies = _companyRepository.GetCompanies();
            return Ok(companies);
        }

        [HttpPost]
        public ActionResult<Company> Create([FromBody] CompanyDTO companyDTO)
        {
            var company = new Company(companyDTO.FantasyName);
            
            if(company.AddDocument(companyDTO.DocumentValue, companyDTO.DocumentType)
                && company.AddFederativeUnit(companyDTO.FederativeUnit))
            {
                var c = _companyRepository.Create(company);

                return CreatedAtRoute("GetCompany", c.CompanyId);
            }

            return BadRequest("Unexpected company format.");
        }
    }
}
