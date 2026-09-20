using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class GoalViewModel(Goal entity, ICommand command) 
        : EntityViewModel(entity, command)
    {
        private string description = entity.Description ?? string.Empty;
        private int? categoryId = entity.CategoryId;

        public string? Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public int? CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                RaisePropertyChanged(nameof(CategoryId));
            }
        }

        // TODO: Implement achievement mapping
        public ObservableCollection<AchievementViewModel> Achievements { get; } = new();

        public void SetCommand(Func<GoalViewModel, ICommand> commandFactory)
        {
            base.SetCommand(commandFactory);
        }
    }
}
