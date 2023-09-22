using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.DTO.Sale;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<SaleDTO>>> GetSales(
            [FromQuery] PaginationParametersDTO paginationParameters)
        {
            var sales = await _saleService.GetSales(paginationParameters);

            var metadata = new
            {
                sales.TotalCount,
                sales.PageSize,
                sales.CurrentPage,
                sales.TotalPages,
                sales.HasNext,
                sales.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSaleById(long id)
        {
            try
            {
                var sales = await _saleService.GetSaleById(id);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult<PagedList<SaleDTO>>> GetSaleByCompanyId(long id,
            [FromQuery] PaginationParametersDTO paginationParameters)
        {
            try
            {
                var sales = await _saleService.GetSaleByCompanyId(paginationParameters, id);
                
                var metadata = new
                {
                    sales.TotalCount,
                    sales.PageSize,
                    sales.CurrentPage,
                    sales.TotalPages,
                    sales.HasNext,
                    sales.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(sales);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponseDTO>> CreateSale(
            [FromBody] SaleDTO request)
        {
            var company = await _saleService.CreateSale(request);
            if (company.Success)
            {
                return Ok(company.Message);
            }
            return BadRequest(company.Message);
        }
    }
}
