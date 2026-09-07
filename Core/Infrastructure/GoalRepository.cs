using CatchLightning.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    internal class GoalRepository : GenericRepository<Goal>, IFindByName<Goal>
    {
        public GoalRepository(DbContext context) : base(context)
        {

        }

        public async Task<Goal?> GetByDescriptionAsync(string description)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Description.Contains(description));
        }
    }
}
