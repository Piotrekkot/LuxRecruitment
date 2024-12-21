using LuxRecruitment.Application.Interfaces;
using LuxRecruitment.Application.Service;
using LuxRecruitment.Core.Interfaces;
using LuxRecruitment.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.Http.Headers;



namespace LuxRecruitment.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddLuxRecruitmentServices(this IServiceCollection services)
        {
            //logger
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            services.AddScoped<IExchangeService, ExchangeService>();

            services.AddHttpClient<INBPApiService, NbpApiService>(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            }); ;

            
        }
    }
}
