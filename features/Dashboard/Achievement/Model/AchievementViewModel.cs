using CatchLightning.Core.Abstractions.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class AchievementViewModel : BaseViewModel
    {
        private string title;
        private string description;
        private bool isCompleted = false;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
    }
}
