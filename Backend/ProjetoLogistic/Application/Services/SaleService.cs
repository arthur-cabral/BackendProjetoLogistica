using Application.DTO.Company;
using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.DTO.Sale;
using Application.HttpClientServices;
using Application.Interfaces;
using Application.RabbitMq;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IHttpClientTransportService _httpClientTransport;

        public SaleService(ISaleRepository saleRepository, 
            ICompanyRepository companyRepository, 
            IMapper mapper,
            IRabbitMqClient rabbitMqClient,
            IHttpClientTransportService httpClientTransport)
        {
            _saleRepository = saleRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _rabbitMqClient = rabbitMqClient;
            _httpClientTransport = httpClientTransport;
        }

        public async Task<PagedList<SaleDTO>> GetSales(PaginationParametersDTO paginationParametersDTO)
        {
            var paginationParametersEntity = _mapper.Map<PaginationParameters>(paginationParametersDTO);
            var saleEntity = await _saleRepository.GetSales(paginationParametersEntity);
            foreach (var sale in saleEntity)
            {
                sale.Company = await _companyRepository.GetCompanyById(sale.CompanyId);
            }
            return _mapper.Map<PagedList<SaleDTO>>(saleEntity);
        }

        public async Task<SaleDTO> GetSaleById(long id)
        {
            var saleEntity = await _saleRepository.GetSaleById(id);
            if (saleEntity == null)
            {
                throw new Exception("Sale not found!");
            }

            var companyEntity = await _companyRepository.GetCompanyById(saleEntity.CompanyId);
            saleEntity.Company = companyEntity;
            return _mapper.Map<SaleDTO>(saleEntity);
        }

        public async Task<PagedList<SaleDTO>> GetSaleByCompanyId(PaginationParametersDTO paginationParametersDTO, long id)
        {
            var existsCompany = await _companyRepository.ExistsCompany(id);
            if (!existsCompany)
            {
                throw new Exception("Company not found");
            }

            var paginationParametersEntity = _mapper.Map<PaginationParameters>(paginationParametersDTO);
            var saleEntity = await _saleRepository.GetSaleByCompanyId(paginationParametersEntity, id);
            foreach (var sale in saleEntity)
            {
                sale.Company = await _companyRepository.GetCompanyById(sale.CompanyId);
            }
            return _mapper.Map<PagedList<SaleDTO>>(saleEntity);
        }

        public async Task<MessageResponseDTO> CreateSale(SaleDTO saleDTO)
        {
            try
            {
                saleDTO.Active = true;
                Company companyEntity = await _companyRepository.GetCompanyById(saleDTO.CompanyId);
                if (companyEntity == null || !companyEntity.Active)
                {
                    return new MessageResponseDTO(false, "Company not found or not active");
                }

                Sale saleEntity = _mapper.Map<Sale>(saleDTO);
                await _saleRepository.CreateSale(saleEntity);

                _rabbitMqClient.CreateTransport(saleDTO);

                return new MessageResponseDTO(true, "Sale created successfully");
            }
            catch (Exception ex)
            {
                return new MessageResponseDTO(false, ex.Message);
            }
        }

        public async Task<MessageResponseDTO> UpdateSale(SaleDTO saleDTO)
        {
            bool existsSale = await _saleRepository.ExistsSale(saleDTO.Id);
            if (!existsSale)
            {
                return new MessageResponseDTO(false, "Sale not found");
            }
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleDTO);
                saleEntity.Id = saleDTO.Id;
                await _saleRepository.UpdateSale(saleEntity);

                return new MessageResponseDTO(true, "Sale updated successfully");
            }
            catch (Exception ex)
            {
                return new MessageResponseDTO(false, ex.Message);
            }
        }

        public async Task<MessageResponseDTO> DeleteSale(long id)
        {
            bool existsSale = await _saleRepository.ExistsSale(id);
            if (!existsSale)
            {
                return new MessageResponseDTO(false, "Sale not found");
            }

            await _saleRepository.DeleteSale(id);
            return new MessageResponseDTO(true);
        }
    }
}
