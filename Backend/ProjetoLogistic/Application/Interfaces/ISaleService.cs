using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.DTO.Sale;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        Task<PagedList<SaleDTO>> GetSales(PaginationParametersDTO paginationParametersDTO);
        Task<SaleDTO> GetSaleById(long id);
        Task<PagedList<SaleDTO>> GetSaleByCompanyId(PaginationParametersDTO paginationParametersDTO, long id);
        Task<MessageResponseDTO> CreateSale(SaleDTO saleDTO);
        Task<MessageResponseDTO> UpdateSale(SaleDTO saleDTO);
        Task<MessageResponseDTO> DeleteSale(long id);
    }
}
