using CatchLightning.Core.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CatchLightning
{
    internal class AppConfig : IConfig
    {
        private IConfiguration configuration;
        public AppConfig()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"),
                optional: false, reloadOnChange: true)
                .Build();
        }

        public string? GetConnectionString(string name = "Default")
        {
            return configuration.GetConnectionString(name);
        }

        public void SetConnectionString(string connectionString, string name = "Default")
        {
            configuration.GetSection("ConnectionStrings")[name] = connectionString;
        }
    }
}
