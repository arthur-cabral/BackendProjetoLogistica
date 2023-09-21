using Application.DTO.Company;
using Application.DTO.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            var companies = await _companyService.GetCompanies();
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyById(long id)
        {
            try
            {
                var companies = await _companyService.GetCompanyById(id);
                return Ok(companies);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponseDTO>> CreateCompany(
            [FromBody] CompanyDTO request)
        {
            var company = await _companyService.CreateCompany(request);
            if (company.Success)
            {
                return Ok(company.Message);
            }
            return BadRequest(company.Message);
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponseDTO>> UpdateCompany(
            [FromBody] CompanyDTO request)
        {
            var company = await _companyService.UpdateCompany(request);
            if (company.Success)
            {
                return Ok(company.Message);
            }
            return BadRequest(company.Message);
        }

        [HttpDelete]
        public async Task<ActionResult<MessageResponseDTO>> DeleteCompany(long id)
        {
            var company = await _companyService.DeleteCompany(id);
            if (company.Success)
            {
                return NoContent();
            }
            return NotFound(company.Message);
        }
    }
}
