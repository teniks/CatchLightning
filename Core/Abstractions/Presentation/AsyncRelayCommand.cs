using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.Core.Abstractions.Presentation
{
    public class AsyncRelayCommand
    {
        private readonly Func<CancellationToken, Task> _execute;
        private readonly Func<bool>? _canExecute;
        private bool isRunning = false;

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute => !isRunning && (_canExecute?.Invoke() ?? true);

        public AsyncRelayCommand(
            Func<CancellationToken, Task> execute,
            Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            if (!CanExecute) return;

            isRunning = true;
            RaiseCanExecuteChanged();

            try
            {
                await _execute(token).ConfigureAwait(true);
            } finally
            {
                isRunning = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
