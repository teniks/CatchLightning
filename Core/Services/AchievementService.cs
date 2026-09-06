using CatchLightning.Core.Infrastructure;
using CatchLightning.Core.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using CatchLightning.Core.Services.Validators;

namespace CatchLightning.Core.Services
{
    internal class AchievementService : IFindByName<Achievement>, IDisposable
    {
        private AchievementRepository achievementRepository;
        private IValidator<Achievement, AchievementError> achievementValidator;

        /// <summary>
        /// <see cref="IValidator{TEntity, TEnumErrors}"/> contains the validation logic for achievement entities. <br/>
        /// And it can be either a single validator or a composite of multiple validators.
        /// </summary>
        /// <param name="achievementRepository"></param>
        /// <param name="validator"></param>
        public AchievementService(AchievementRepository achievementRepository, IValidator<Achievement, AchievementError> validator)
        {
            this.achievementRepository = achievementRepository;
            this.achievementValidator = validator;
        }

        public async Task<Achievement?> GetByIdAsync(int id)
        {
            return await achievementRepository.GetByIDAsync(id);
        }

        public async Task<IQueryable<Achievement>> GetAsync(int count, int skip = 0)
        {
            return await achievementRepository.GetPartFromAsync(count, skip);
        }

        public async Task<Achievement?> GetByNameAsync(string name)
        {
            return await achievementRepository.GetByNameAsync(name);
        }

        public async Task<Achievement?> GetByDescriptionAsync(string description)
        {
            return await achievementRepository.GetByDescriptionAsync(description);
        }

        /// <summary>
        /// Adds a new achievement to the repository after validating it. <br/>
        /// If the validation fails, it returns an <see cref="OperationResult{TEnumErrors}"/> with the error.
        /// </summary>
        /// <param name="achievement"></param>
        /// <returns></returns>
        public async Task<OperationResult<AchievementError>> AddAsync(Achievement achievement)
        {
            AchievementError? result = await achievementValidator.Validate(achievement);
            if (result != null)
                return OperationResult<AchievementError>.Failure(result.Value);

            await achievementRepository.Add(achievement);
            achievementRepository.SaveChanges();
            return OperationResult<AchievementError>.Success();
        }

        /// <summary>
        /// Updates an existing achievement in the repository after validating it. <br/>
        /// If the validation fails, it returns an <see cref="OperationResult{TEnumErrors}"/> with the error.
        /// </summary>
        /// <param name="achievement"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Achievement achievement)
        {
            await achievementRepository.Update(achievement);
            achievementRepository.SaveChanges();
        }

        public void Delete(Achievement achievement)
        {
            achievementRepository.Delete(achievement);
            achievementRepository.SaveChanges();
        }

        public void Dispose()
        {
            achievementRepository.Dispose();
        }
    }
}
