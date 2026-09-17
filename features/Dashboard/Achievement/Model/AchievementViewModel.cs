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
                RaisePropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
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
    }
}
