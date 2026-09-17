using CatchLightning.Core.Abstractions.Presentation;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class CategoryViewModel : EntityViewModel
    {
        private string title;
        private bool isSelected = false;


        public required string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
