using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CatchLightning.Core.Infrastructure
{
    
    internal class SqliteContext : DbContext
    {
        private IConfig _config;
        private string? connectionString;

        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Goal> Goals { get; set; }
        public DbSet<Models.Achievement> Achivments { get; set; }


        public SqliteContext(IConfig config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(GetConnectionString());
        }

        /// <summary>
        /// Gets the <i>connection string</i> from the configuration.
        /// <br/>
        /// If the connection string is not set, it will create a <b>default</b> connection string
        /// at the current directory with the name <c>sqlite.db</c> and set it in the configuration.
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            connectionString = _config.GetConnectionString();

            if (connectionString == null || connectionString == "")
            {
                connectionString = $"DataSource={Directory.GetCurrentDirectory()}/sqlite.db";
                _config.SetConnectionString(connectionString);
            }

            return connectionString;
        }
    }
}
