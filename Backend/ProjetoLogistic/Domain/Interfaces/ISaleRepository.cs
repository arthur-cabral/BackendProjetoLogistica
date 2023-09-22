using Domain.Entities;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<PagedList<Sale>> GetSales(PaginationParameters paginationParameters);
        Task<Sale> GetSaleById(long id);
        Task<PagedList<Sale>> GetSaleByCompanyId(PaginationParameters paginationParameters, long id);
        Task<bool> ExistsSale(long id);
        Task CreateSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task DeleteSale(long id);
    }
}
