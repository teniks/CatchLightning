using Avalonia.Threading;
using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.features.Dashboard.Achievement.Model;
using CatchLightning.features.Dashboard.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatchLightning.features.Dashboard.Model
{
    public class MainWindowViewModel : BaseViewModel, IDisposable
    {
        public NavigationTree NavigationTree { get; set; }
        public BaseViewModel? CurrentItemViewModel { get; set; }
        private readonly IMediator _mediator;
        private readonly IDisposable _sub;
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));

                if (value < 0 || value >= NavigationTree.NavigationPaths.Count) return;

                var path = NavigationTree.NavigationPaths[value];

                Dispatcher.UIThread.Post(() =>
                {
                    NavigationTree.ActivateNavigationPath(path);
                });

            }
        }

        private BaseViewModel _currentPage;
        public BaseViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                RaisePropertyChanged(nameof(CurrentPage));
            }
        }
        
        public MainWindowViewModel(IMediator mediator)
        {
            _mediator = mediator;
            _sub = mediator.Subscribe<EntitySelect>(OnEntitySelect);

            NavigationTree = new();
            CurrentPage = new CategoryPageViewModel(mediator);
        }

        internal void OnEntitySelect(EntitySelect entity)
        {
            NavigationTree.AddNavigationPath( (entity.entity) switch
            {
                CategoryViewModel category => new NavigationPath
                {
                    Name = category.Title,
                    Entity = entity.entity,
                    LevelDepth = NavigationPath.Depth.Category,
                    Command = new AsyncRelayCommand(
                        async CancellationToken =>
                        {
                            NavigationTree.ReplacePath(SaveUntil(SelectedIndex));
                            _mediator.Publish(new EntityRevoke(entity.entity));
                        })
                },

                GoalViewModel goal => new NavigationPath
                {
                    Name = goal.Title,
                    Entity = entity.entity,
                    LevelDepth = NavigationPath.Depth.Goal,
                    Command = new AsyncRelayCommand(
                        async CancellationToken =>
                        {
                            NavigationTree.ReplacePath(SaveUntil(SelectedIndex));
                            _mediator.Publish(new EntityRevoke(entity.entity));
                        })
                }
            });
        }

        private List<NavigationPath> SaveUntil(int index)
        {
            var olditems = NavigationTree.NavigationPaths.ToArray();
            List<NavigationPath> items = new();

            int max = Math.Max(0, Math.Min(index, olditems.Length));
            for (int i = 0; i < max; i++)
            {
                items.Add(olditems[i]);
            }

            return items;
        }

        public void Dispose() => 
            _sub.Dispose();
    }
}
