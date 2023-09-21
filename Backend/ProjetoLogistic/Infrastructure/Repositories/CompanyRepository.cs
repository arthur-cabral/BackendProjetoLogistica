using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await Get().ToListAsync();
        }

        public async Task<Company> GetCompanyById(long id)
        {
            return await GetByProperty((x) => x.Id == id);
        }
        
        public async Task<bool> ExistsCompany(long id)
        {
            Company company = await GetByProperty((x) => x.Id == id);
            return company != null;
        }

        public async Task CreateCompany(Company company)
        {
            Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(Company company)
        {
            Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(long id)
        {
            Company company = await GetByProperty((x) => x.Id == id);
            Delete(company);
            await _context.SaveChangesAsync();
        }
    }
}
