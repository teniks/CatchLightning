using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CatchLightning.Core.Abstractions.Presentation
{
    /// <summary>
    /// ViewModel abstraction for the entity model.
    /// </summary>
    public abstract class EntityViewModel : BaseViewModel
    {
        protected readonly int id;
        public ICommand Command { get; set; }


        public required int Id
        {
            get => id;
            init => id = value;
        }
    }
}
