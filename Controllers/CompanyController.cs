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
        public IActionResult Get(int id)
        {
            var company = _companyRepository.GetCompany(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var companies = _companyRepository.GetCompanies();
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CompanyDTO companyDTO)
        {
            var company = new Company(companyDTO.FantasyName);
            
            if(company.AddDocument(companyDTO.DocumentValue, companyDTO.DocumentType)
                && company.AddFederativeUnit(companyDTO.FederativeUnit))
            {
                var c = _companyRepository.Create(company);

                return CreatedAtRoute("GetCompany", new { Id = c.CompanyId });
            }

            return BadRequest("Unexpected company format.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompanyDTO companyDTO)
        {
            var company = new Company(companyDTO.FantasyName);

            if (company.AddDocument(companyDTO.DocumentValue, companyDTO.DocumentType)
                && company.AddFederativeUnit(companyDTO.FederativeUnit))
            {
                _companyRepository.Update(id, company);

                return NoContent();
            }

            return BadRequest("Unexpected company format.");
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            if (!_companyRepository.CompanyExists(id))
                return NotFound();

            _companyRepository.Remove(id);

            return NoContent();
        }
    }
}
