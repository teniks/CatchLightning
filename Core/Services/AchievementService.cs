using CatchLightning.Core.Infrastructure;
using CatchLightning.Core.Models;
using System.Threading.Tasks;
using CatchLightning.Core.Services.Validators;
using System.Collections.Generic;
using CatchLightning.Core.Models.Filters;

namespace CatchLightning.Core.Services
{
    internal class AchievementService : EntityService<Achievement, AchievementError>
    {
        public AchievementService(AchievementRepository repository, IValidator<Achievement, AchievementError> validator) : base (repository, validator)
        {

        }

        public async Task<List<Achievement>?> GetByDescriptionAsync(string description)
        {
            return await (repository as AchievementRepository).GetByDescriptionAsync(description);
        }

        public async Task<List<Achievement>> GetByFilter(AchievementFilter filter)
        {
            return await (repository as AchievementRepository).GetByFilter(filter);
        }
    }
}
