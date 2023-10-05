using Application.DTO.Sale;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.HttpClientServices
{
    public class HttpClientTransportService : IHttpClientTransportService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HttpClientTransportService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async void CreateTransportRequest(SaleDTO saleDTO)
        {
            var content = new StringContent
                (
                    JsonSerializer.Serialize(saleDTO),
                    Encoding.UTF8,
                    "application/json"
                );

            await _client.PostAsync(_configuration["TransportService"], content);
        }
    }
}
