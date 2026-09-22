using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CatchLightning.Core.Abstractions.Presentation
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<CancellationToken, Task> _execute;
        private readonly Func<bool>? _canExecute;
        private readonly Lock _gate = new();
        private readonly SynchronizationContext? _syncContext;
        private readonly Action<Exception>? _onException;
        private bool _isRunning = false;

        public event EventHandler? CanExecuteChanged;


        /// <summary>
        /// Remembers <see cref="SynchronizationContext"/> to invole handler for <see cref="CanExecuteChanged"/>
        /// </summary>
        public AsyncRelayCommand(
            Func<CancellationToken, Task> execute,
            Func<bool>? canExecute = null,
            Action<Exception>? onException = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _onException = onException;
            _syncContext = SynchronizationContext.Current;
        }

        public AsyncRelayCommand(
            Func<Task> execute,
            Func<bool>? canExecute = null) : 
            this(_ => execute(), canExecute) { }

        public bool CanExecute(object? parameter)
        {
            lock (_gate)
            {
                if (_isRunning) return false;
            }
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            _ = ExecuteAsync(CancellationToken.None);
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            if (!TryEnter()) return;

            try
            {
                await _execute(token);
            } 
            catch (OperationCanceledException _) when (token.IsCancellationRequested)
            {

            } 
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }
            finally
            {
                Exit();
            }
        }

        private bool TryEnter()
        {
            lock (_gate)
            {
                if (_isRunning) return false;
                _isRunning = true;
            }
            RaiseCanExecuteChanged();
            return true;
        }

        private void Exit()
        {
            lock (_gate)
            {
                _isRunning = false;
            }
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler is null) return;

            if (_syncContext is null || _syncContext == SynchronizationContext.Current)
                handler(this, EventArgs.Empty);
            else
                _syncContext.Post(_ => handler(this, EventArgs.Empty), null);
        }
    }
}
