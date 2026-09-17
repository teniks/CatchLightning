using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatchLightning.Core.Abstractions.Presentation
{
    /// <summary>
    /// ViewModel abstraction general purpose.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
