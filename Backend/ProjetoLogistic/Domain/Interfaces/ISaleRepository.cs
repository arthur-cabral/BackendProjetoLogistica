using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetSales();
        Task<Sale> GetSaleById(long id);
        Task<bool> ExistsSale(long id);
        Task CreateSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task DeleteSale(long id);
    }
}
