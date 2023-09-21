using Application.DTO.Company;
using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CompanyDTO>> GetCompanies(PaginationParametersDTO paginationParametersDTO)
        {
            var paginationParametersEntity = _mapper.Map<PaginationParameters>(paginationParametersDTO);
            var companyEntity = await _companyRepository.GetCompanies(paginationParametersEntity);
            return _mapper.Map<PagedList<CompanyDTO>>(companyEntity);
        }

        public async Task<CompanyDTO> GetCompanyById(long id)
        {
            var companyEntity = await _companyRepository.GetCompanyById(id);
            if (companyEntity == null)
            {
                throw new Exception("Company not found!");
            }
            return _mapper.Map<CompanyDTO>(companyEntity);
        }

        public async Task<MessageResponseDTO> CreateCompany(CompanyDTO companyDTO)
        {
            try
            {
                var companyEntity = _mapper.Map<Company>(companyDTO);
                await _companyRepository.CreateCompany(companyEntity);

                return new MessageResponseDTO(true, "Company created successfully");
            } catch (Exception ex)
            {
                return new MessageResponseDTO(false, ex.Message);
            }
        }
        
        public async Task<MessageResponseDTO> UpdateCompany(CompanyDTO companyDTO)
        {
            bool existsCompany = await _companyRepository.ExistsCompany(companyDTO.Id);
            if (!existsCompany) 
            {
                return new MessageResponseDTO(false, "Company not found");
            }
            try
            {
                var companyEntity = _mapper.Map<Company>(companyDTO);
                companyEntity.Id = companyDTO.Id;
                await _companyRepository.UpdateCompany(companyEntity);

                return new MessageResponseDTO(true, "Company updated successfully");
            } catch (Exception ex)
            {
                return new MessageResponseDTO(false, ex.Message);
            }
        }

        public async Task<MessageResponseDTO> DeleteCompany(long id)
        {
            bool existsCompany = await _companyRepository.ExistsCompany(id);
            if (!existsCompany)
            {
                return new MessageResponseDTO(false, "Company not found");
            }

            await _companyRepository.DeleteCompany(id);
            return new MessageResponseDTO(true);
        }

    }
}
