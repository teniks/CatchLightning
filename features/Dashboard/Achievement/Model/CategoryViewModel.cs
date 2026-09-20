using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using System;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Achievement.Model
{
    public class CategoryViewModel(Category entity, ICommand command) 
        : EntityViewModel(entity, command)
    {
        private bool isSelected = false;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        public void SetCommand(Func<CategoryViewModel, ICommand> commandFactory)
        {
            base.SetCommand(commandFactory);
        }
    }
}
