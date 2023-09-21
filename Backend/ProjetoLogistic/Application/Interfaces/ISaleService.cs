using Application.DTO.Response;
using Application.DTO.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDTO>> GetSales();
        Task<SaleDTO> GetSaleById(long id);
        Task<MessageResponseDTO> CreateSale(SaleDTO saleDTO);
        Task<MessageResponseDTO> UpdateSale(SaleDTO saleDTO);
        Task<MessageResponseDTO> DeleteSale(long id);
    }
}
