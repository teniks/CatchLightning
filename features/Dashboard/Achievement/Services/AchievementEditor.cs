using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using CoreModels = CatchLightning.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Achievement.Services
{
    public class AchievementEditor : IEntityEditor<AchievementViewModel, AchievementError>
    {
        private EntityService<CoreModels.Achievement, AchievementError> _service;

        public AchievementEditor(
            EntityService<CoreModels.Achievement, AchievementError> service)
        {
            _service = service;
        }

        public async Task<OperationResult<AchievementError>> CreateAsync(AchievementViewModel viewModel, CancellationToken cancel = default)
        {

            return await _service.AddAsync(new CoreModels.Achievement
            {
                Name = viewModel.Title,
                Description = viewModel.Description,
                Level = viewModel.Level,
                IsCompleted = viewModel.IsCompleted,

                CategoryId = viewModel.CategoryId,
                GoalId = viewModel.GoalId
            });
        }

        public async Task<OperationResult<AchievementError>> UpdateAsync(AchievementViewModel viewModel, CancellationToken cancel = default)
        {
            return await _service.UpdateAsync(viewModel.Id, (achievement) =>
            {
                achievement.Name = viewModel.Title;
                achievement.Description = viewModel.Description;
                achievement.Level = viewModel.Level;
                achievement.IsCompleted = viewModel.IsCompleted;

                achievement.CategoryId = viewModel.CategoryId;
                achievement.GoalId = viewModel.GoalId;
            });
        }
    }
}
