using Application.DTO.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RabbitMq
{
    public interface IRabbitMqClient
    {
        void CreateTransport(SaleDTO saleDTO);
    }
}
