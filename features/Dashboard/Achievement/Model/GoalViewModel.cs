using CatchLightning.Core.Abstractions.Presentation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class GoalViewModel : EntityViewModel
    {
        private string title;
        private string? description;

        public required string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string? Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public ObservableCollection<AchievementViewModel> Achievements { get; } = new();
    }
}
