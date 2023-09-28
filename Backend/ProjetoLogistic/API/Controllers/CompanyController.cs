using Application.DTO.Company;
using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.Interfaces;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public async Task<ActionResult<PagedList<CompanyDTO>>> GetCompanies(
            [FromQuery] PaginationParametersDTO paginationParameters)
        {
            var companies = await _companyService.GetCompanies(paginationParameters);
            AddingXPaginationHeaders(companies);
            return Ok(companies);
        }

        private void AddingXPaginationHeaders(PagedList<CompanyDTO> companies)
        {
            var metadata = new
            {
                companies.TotalCount,
                companies.PageSize,
                companies.CurrentPage,
                companies.TotalPages,
                companies.HasNext,
                companies.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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
