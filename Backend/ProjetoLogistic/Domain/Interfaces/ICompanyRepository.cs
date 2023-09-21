using Domain.Entities;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<PagedList<Company>> GetCompanies(PaginationParameters paginationParameters);
        Task<Company> GetCompanyById(long id);
        Task<bool> ExistsCompany(long id);
        Task CreateCompany(Company company);
        Task UpdateCompany(Company company);
        Task DeleteCompany(long id);
    }
}
