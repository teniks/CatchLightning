using CatchLightning.Core.Models;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Achievement.Services
{
    public class GoalEditor : IEntityEditor<GoalViewModel, GoalError>
    {

        private EntityService<Goal, GoalError> _service;


        public GoalEditor(
            EntityService<Goal, GoalError> service)
        {
            _service = service;
        }

        public async Task<OperationResult<GoalError>> CreateAsync(GoalViewModel viewModel, CancellationToken cancel = default)
        {
            return await _service.AddAsync(new Goal
            {
                Name = viewModel.Title,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId
            });
        }

        public async Task<OperationResult<GoalError>> UpdateAsync(GoalViewModel viewModel, CancellationToken cancel = default)
        {
            return await _service.UpdateAsync(viewModel.Id, (goal) =>
            {
                goal.Name = viewModel.Title;
                goal.Description = viewModel.Description;
                goal.CategoryId = viewModel.CategoryId;
            });
        }
    }
}
