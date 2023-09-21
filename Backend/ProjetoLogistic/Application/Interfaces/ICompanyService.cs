using Application.DTO.Company;
using Application.DTO.Pagination;
using Application.DTO.Response;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<PagedList<CompanyDTO>> GetCompanies(PaginationParametersDTO paginationParameters);
        Task<CompanyDTO> GetCompanyById(long id);
        Task<MessageResponseDTO> CreateCompany(CompanyDTO companyDTO);
        Task<MessageResponseDTO> UpdateCompany(CompanyDTO companyDTO);
        Task<MessageResponseDTO> DeleteCompany(long id);
    }
}
