using Microsoft.EntityFrameworkCore;

namespace CatchLightning.Core.Infrastructure
{

    internal class SqliteContext : DbContext
    {
        private IConfig _config;

        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Goal> Goals { get; set; }
        public DbSet<Models.Achievement> Achivments { get; set; }


        public SqliteContext(IConfig config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.GetConnectionString());
        }
    }
}
