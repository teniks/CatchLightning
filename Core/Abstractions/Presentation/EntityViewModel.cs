using CatchLightning.Core.Models;
using System;
using System.Windows.Input;

namespace CatchLightning.Core.Abstractions.Presentation
{
    /// <summary>
    /// ViewModel abstraction for the entity model.
    /// </summary>
    public abstract class EntityViewModel(IEntity entity, ICommand command) : BaseViewModel
    {
        protected readonly int id = entity.Id;
        private string title = entity.Name;
        public ICommand Command { get; protected set; } = command;


        public int Id
        {
            get => id;
            init => id = value;
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        protected virtual void SetCommand<TSelf>(Func<TSelf, ICommand> commandFactory) where TSelf : EntityViewModel
        {
            Command = commandFactory((TSelf)this);
        }
    }
}
