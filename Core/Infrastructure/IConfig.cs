using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning.Core.Infrastructure
{
    internal interface IConfig
    {
        public string? GetConnectionString(string name = "Default");
        public void SetConnectionString(string connectionString, string name = "Default");
    }
}
