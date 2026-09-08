using CatchLightning.Core.Models;
using CatchLightning.Core.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    internal class AchievementRepository : GenericRepository<Achievement>, IFindByName<Achievement>
    {
        public AchievementRepository(SqliteContext context) : base(context)
        {

        }

        public async Task<List<Achievement>?> GetByDescriptionAsync(string description, int count = 30, int skip = 0)
        {
            return await dbSet.Where(x => x.Description != null && x.Description.Contains(description)).Skip(skip).Take(count).ToListAsync();
        }

        public async Task AddDependentAchievment(Achievement current, Achievement dependent)
        {
            (await dbSet.FirstOrDefaultAsync(x => x.Equals(current)))?.Dependents.Add(dependent);
        }

        public async Task AddGoal(Achievement current, Goal goal)
        {
            (await dbSet.FirstOrDefaultAsync(x => x.Equals(current)))?.GoalId = goal.Id;
        }

        public async Task AddCategory(Achievement current, Category category)
        {
            (await dbSet.FirstOrDefaultAsync(x => x.Equals(current)))?.CategoryId = category.Id;
        }

        public async Task<List<Achievement>> GetByFilter(AchievementFilter filter, int count = 30, int skip = 0)
        {
            IQueryable<Achievement> query = dbSet;

            if (filter.IsCompleted.HasValue)
                query.Where(w => w.IsCompleted == filter.IsCompleted);

            if (filter.CategoryId.HasValue)
                query.Where(w => w.CategoryId.HasValue && w.CategoryId == filter.CategoryId);

            if (filter.GoalIds?.Count > 0)
                query.Where(w => w.GoalId.HasValue && filter.GoalIds.Contains(w.GoalId.Value));

            return await query.Skip(skip).Take(count).ToListAsync();
        }
    }
} 
