using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using CatchLightning.features.Dashboard.Achievement.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Create.Model
{
    public class EditingGoalViewModel : BaseEditungViewModel
    {
        private IEntityEditor<GoalViewModel, GoalError> _goalEditor;
        protected override ObservableCollection<FieldViewModel> _availableFields { get; } = new()
        {
            new DescriptionFieldViewModel(),
            new CategoryFieldViewModel()
        };

        public EditingGoalViewModel(
                IEntityEditor<GoalViewModel, GoalError> goalEditor
            )
        {
            _goalEditor = goalEditor;
        }

        public void SetFieldsFor(GoalViewModel goal)
        {
            Title                                       = goal.Title;
            GetField<DescriptionFieldViewModel>().Value = goal.Description ?? string.Empty;

            GetField<CategoryFieldViewModel>().Value    = GetField<CategoryFieldViewModel>().Options
                                                            .FirstOrDefault(f => f.Id.Equals(goal.CategoryId));
        }

        public override async Task<OperationResult> CreateAsync()
        {
            return await _goalEditor.CreateAsync(ToViewModel());
        }

        public override async Task<OperationResult> UpdateAsync()
        {
            return await _goalEditor.UpdateAsync(ToViewModel());
        }

        private GoalViewModel ToViewModel()
        {
            return new GoalViewModel(new Core.Models.Goal
            {
                Id          = this.Id,
                Name        = this.Title,
                Description = GetField<DescriptionFieldViewModel>().Value,
                CategoryId  = GetField<CategoryFieldViewModel>().Value?.Id
            }, null);
        }
    }
}
