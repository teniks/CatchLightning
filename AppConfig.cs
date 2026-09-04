using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning
{
    internal class AppConfig
    {
        private IConfiguration configuration; 
        public AppConfig() 
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
