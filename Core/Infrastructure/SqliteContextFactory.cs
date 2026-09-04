using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning.Core.Infrastructure
{
    /// <summary>
    /// <see cref="SqliteContextFactory"/> is a factory class that implements the <see cref="IDesignTimeDbContextFactory{TContext}"/> interface.
    /// <br/>
    /// Without this factory, the <b>EF Core</b> command would not be able to create a <see cref="SqliteContext"/> instance at <i>design time</i>.
    /// </summary>
    internal class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteContext>
    {
        public SqliteContext CreateDbContext(string[] args)
        {
            var config = new AppConfig();
            return new SqliteContext(config);
        }
    }
}
