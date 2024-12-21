using Microsoft.Extensions.Configuration;
using Serilog;

namespace LuxRecruitment.Logging
{
    public static class LoggerConfigurator
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
