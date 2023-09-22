using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Sale>> GetSales(PaginationParameters paginationParameters)
        {
            return PagedList<Sale>.ToPagedList(Get().OrderBy(on => on.Id),
                paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<Sale> GetSaleById(long id)
        {
            return await GetByProperty((x) => x.Id == id);
        }

        public async Task<PagedList<Sale>> GetSaleByCompanyId(PaginationParameters paginationParameters, long id)
        {
            return PagedList<Sale>.ToPagedList(Get().Where((x) => x.Company.Id == id),
                paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<bool> ExistsSale(long id)
        {
            Sale sale = await GetByProperty((x) => x.Id == id);
            return sale != null;
        }

        public async Task CreateSale(Sale sale)
        {
            Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSale(Sale sale)
        {
            Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSale(long id)
        {
            Sale sale = await GetByProperty((x) => x.Id == id);
            Delete(sale);
            await _context.SaveChangesAsync();
        }

        
    }
}
