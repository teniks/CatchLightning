using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using Models = CatchLightning.Core.Models;
using System;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class AchievementViewModel(Models.Achievement entity, ICommand command)
        : EntityViewModel(entity, command)
    {
        private string description = entity.Description ?? string.Empty;
        private AchievementLevel level = entity.Level;
        private bool isCompleted = entity.IsCompleted;
        private int? categoryId = entity.CategoryId;
        private int? goalId = entity.GoalId;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public AchievementLevel Level
        {
            get => level;
            set
            {
                level = value;
                RaisePropertyChanged(nameof(Level));
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                RaisePropertyChanged(nameof(IsCompleted));
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

        public int? GoalId
        {
            get => goalId;
            set
            {
                goalId = value;
                RaisePropertyChanged(nameof(GoalId));
            }
        }

        public void SetCommand(Func<AchievementViewModel, ICommand> commandFactory)
        {
            base.SetCommand(commandFactory);
        }
    }
}
