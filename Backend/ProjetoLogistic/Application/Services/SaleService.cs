using Application.DTO.Company;
using Application.DTO.Response;
using Application.DTO.Sale;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
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
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleDTO>> GetSales()
        {
            var saleEntity = await _saleRepository.GetSales();
            return _mapper.Map<IEnumerable<SaleDTO>>(saleEntity);
        }

        public async Task<SaleDTO> GetSaleById(long id)
        {
            var saleEntity = await _saleRepository.GetSaleById(id);
            if (saleEntity == null)
            {
                throw new Exception("Sale not found!");
            }
            return _mapper.Map<SaleDTO>(saleEntity);
        }

        public async Task<MessageResponseDTO> CreateSale(SaleDTO saleDTO)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleDTO);
                await _saleRepository.CreateSale(saleEntity);

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
