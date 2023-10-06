using Application.HttpClientServices;
using Application.Interfaces;
using Application.Mapping;
using Application.RabbitMq;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(
                    defaultConnection,
                    new MySqlServerVersion(new Version(8, 0, 33)),
                    b => b.MigrationsAssembly("API")
                );

            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ISaleService, SaleService>();
            
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            services.AddHttpClient<IHttpClientTransportService, HttpClientTransportService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
