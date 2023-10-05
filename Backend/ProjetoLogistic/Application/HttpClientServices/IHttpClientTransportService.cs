using Application.DTO.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HttpClientServices
{
    public interface IHttpClientTransportService
    {
        public void CreateTransportRequest(SaleDTO saleDTO);
    }
}
