using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.NavigationPanel
{
    public class NavigationTree : IDisposable, INotifyPropertyChanged
    {
        //TODO: implement a snapshot pattern for NavigationPaths to stabilize data changes
        public ObservableCollection<NavigationPath> _navigationPaths = new();
        public ObservableCollection<NavigationPath> NavigationPaths
        {
            get => _navigationPaths;
            set
            {
                _navigationPaths = value;
                RaisePropertyChanged(nameof(NavigationPaths));
            }
        }
        private CancellationTokenSource loadCancellationTokenSource { get; set; } = new();
        private bool _isLoading = false;
        private bool _disposed = false;
        private readonly Lock gate = new Lock();

        /// <summary>
        /// Async data event = <see cref="IsLoading"/>
        /// </summary>
        public event EventHandler? IsLoadingChanged;
        public event PropertyChangedEventHandler? PropertyChanged;


        public void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value) return;

                _isLoading = value;

                IsLoadingChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddNavigationPath(NavigationPath path)
        {
            NavigationPaths.Add(path);
        }

        public void ActivateNavigationPath(NavigationPath selected)
        {
            _ = LoadAsync(selected);
        }

        private async Task LoadAsync(NavigationPath selected)
        {
            // Cancel current loading operation
            var old = loadCancellationTokenSource;

            CancellationTokenSource currentTokenSource = new CancellationTokenSource();
            loadCancellationTokenSource = currentTokenSource;

            old.Cancel();
            old.Dispose();

            IsLoading = true;
            try
            {
                await selected.Command.ExecuteAsync(currentTokenSource.Token);
            }
            catch (OperationCanceledException)
            {

            } finally
            {
                // If the TokenSource is changed, it is not our operation.
                if(ReferenceEquals(currentTokenSource, loadCancellationTokenSource))
                    IsLoading = false;
            }
        }

        /// <summary>
        /// Clear old items and replace them with new <see cref="NavigationPaths"/>
        /// </summary>
        /// <param name="newPaths"></param>
        public void ReplacePath(IReadOnlyCollection<NavigationPath> newPaths)
        {
            Dispatcher.UIThread.Post(() =>
            {
                NavigationPaths = new(newPaths);
            });
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            loadCancellationTokenSource.Cancel();
            loadCancellationTokenSource.Dispose();
        }
    }
}
