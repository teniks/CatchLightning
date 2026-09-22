using Avalonia;
using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using CoreModels = CatchLightning.Core.Models;
using CatchLightning.features.Dashboard.Achievement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CatchLightning.features.Dashboard.Create.Model
{
    public class EditingAchievementViewModel : BaseEditungViewModel
    {
        private EntityService<Goal, GoalError> _goalService;
        private EntityService<Category, CategoryError> _categoryService;
        private IEntityEditor<AchievementViewModel, AchievementError> _achievementEditor;
        protected override ObservableCollection<FieldViewModel> _availableFields { get; } = new()
        {
            new DescriptionFieldViewModel(),
            new CategoryFieldViewModel(),
            new GoalFieldViewModel(),
            new LevelFieldViewModel(),
            new CompletionStatusFieldViewModel()
        };


        public EditingAchievementViewModel(
                IEntityEditor<AchievementViewModel, AchievementError> achievementEditor,
                EntityService<Category, CategoryError> categoryService,
                EntityService<Goal, GoalError> goalService
            )
        {
            _achievementEditor = achievementEditor;
            _categoryService = categoryService;
            _goalService = goalService;
        }


        public async Task LoadMoreCategoriesAsync(int countRecords = 15, int alreadyLoaded = 0)
        {
            IEnumerable<Category> newCategories = await _categoryService.GetAsync(countRecords, alreadyLoaded);

            var first = new Category()
            {
                Id      = -1,
                Name    = "НЕТ КАТЕГОРИИ"
            };

            newCategories = new[] { first }.Concat([first]);

            foreach (var category in newCategories)
            {
                GetField<CategoryFieldViewModel>().Options.Add(new CategoryViewModel(category, null));
            }
        }

        public async Task LoadMoreGoalsAsync(int countRecords = 15, int alreadyLoaded = 0)
        {
            var newGoals = await _goalService.GetAsync(countRecords, alreadyLoaded);

            var first = new Goal()
            {
                Id          = -1,
                Name        = "НЕТ ЦЕЛИ",
                Description = "Не выбрана Цель"
            };
            newGoals = new[] { first }.Concat(newGoals);

            foreach (var goal in newGoals)
            {
                GetField<GoalFieldViewModel>().Options.Add(new GoalViewModel(goal, null));
            }
        }

        public void SetFieldsFor(AchievementViewModel achievement)
        {
            Id                                                  = achievement.Id;
            Title                                               = achievement.Title;
            GetField<DescriptionFieldViewModel>().Value         = achievement.Description ?? string.Empty;
            GetField<LevelFieldViewModel>().Value               = achievement.Level;
            GetField<CompletionStatusFieldViewModel>().Value    = achievement.IsCompleted;


            GetField<CategoryFieldViewModel>().Value            = GetField<CategoryFieldViewModel>().Options
                                                                    .FirstOrDefault(f => f.Id.Equals(achievement.CategoryId));


            GetField<GoalFieldViewModel>().Value                = GetField<GoalFieldViewModel>().Options
                                                                    .FirstOrDefault(f => f.Id.Equals(achievement.GoalId));
        }

        public override async Task<OperationResult> CreateAsync()
        {
            return await _achievementEditor.CreateAsync(ToViewModel());
        }

        public override async Task<OperationResult> UpdateAsync()
        {
            return await _achievementEditor.UpdateAsync(ToViewModel());
        }

        private AchievementViewModel ToViewModel()
        {
            return new AchievementViewModel(new CoreModels.Achievement
            {
                Id          = this.Id,
                Name        = this.Title,
                Description = GetField<DescriptionFieldViewModel>().Value,
                IsCompleted = GetField<CompletionStatusFieldViewModel>().Value,
                Level       = GetField<LevelFieldViewModel>().Value,

                CategoryId  = GetField<CategoryFieldViewModel>().Value?.Id,
                GoalId      = GetField<GoalFieldViewModel>().Value?.Id

            }, new RelayCommand(null));
        }
    }
}
