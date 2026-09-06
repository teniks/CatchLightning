using CatchLightning.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    internal class AchievementRepository : GenericRepository<Achievement>, IFindByName<Achievement>
    {
        public AchievementRepository(SqliteContext context) : base(context)
        {

        }

        public async Task<Achievement?> GetByDescriptionAsync(string description)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Description.Contains(description));
        }

    }
}
