using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.NavigationPanel
{
    public class NavigationTree : IDisposable
    {
        public ObservableCollection<NavigationPath> NavigationPaths { get; } = new();
        private CancellationTokenSource loadCancellationTokenSource { get; set; } = new();
        private bool _isLoading = false;
        private bool _disposed = false;
        private readonly Lock gate = new Lock();

        /// <summary>
        /// Async data event = <see cref="IsLoading"/>
        /// </summary>
        public event EventHandler? IsLoadingChanged;
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
            lock(gate)
            {
                NavigationPaths.Clear();
                foreach (var path in newPaths)
                    NavigationPaths.Add(path);
            }
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
