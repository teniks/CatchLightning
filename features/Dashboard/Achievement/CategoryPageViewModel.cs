using CatchLightning.Core.Abstractions;
using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using CatchLightning.features.Dashboard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Achievement
{
    public class CategoryPageViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryViewModel> Categories { get; } = new();
        public ObservableCollection<GoalViewModel> Goals { get; } = new();
        private CategoryViewModel? _selectedCategory;
        private EntityService<Category, CategoryError> _categoryService;
        private EntityService<Goal, GoalError> _goalService;
        private readonly IMediator _mediator;

        public RelayCommand CreateCategoryCommand { get; }
        public RelayCommand DeleteCategoryCommand { get; }
        public RelayCommand EditCategoryCommand { get; }
        public RelayCommand LoadMoreCategoriesCommand { get; }
        public RelayCommand LoadMoreGoalsCommand { get; }

        private string _newCategoryTitle = string.Empty;
        public string NewCategoryTitle
        {
            get => _newCategoryTitle;
            set
            {
                _newCategoryTitle = value;
                RaisePropertyChanged(nameof(NewCategoryTitle));
            }
        }

        public CategoryPageViewModel(IMediator mediator, EntityService<Category, CategoryError>? categoryService = null, EntityService<Goal, GoalError>? goalService = null)
        {
            _mediator = mediator;
            _categoryService = categoryService;
            _goalService = goalService;

            CreateCategoryCommand = new RelayCommand(() => _ = CreateCategoryAsync());
            DeleteCategoryCommand = new RelayCommand(() => _ = DeleteSelectedCategoryAsync(), () => SelectedCategory != null);
            EditCategoryCommand = new RelayCommand(() => _ = EditSelectedCategoryAsync(), () => SelectedCategory != null);
            LoadMoreCategoriesCommand = new RelayCommand(() => _ = LoadMoreCategories());
            LoadMoreGoalsCommand = new RelayCommand(() => _ = LoadMoreGoals());
        }

        private object EditSelectedCategoryAsync()
        {
            //TODO: Implement editing selected category
            throw new NotImplementedException();
        }

        private object DeleteSelectedCategoryAsync()
        {
            //TODO: Implement remove selected category
            throw new NotImplementedException();
        }

        private object CreateCategoryAsync()
        {
            //TODO: Implement create a new category
            throw new NotImplementedException();
        }

        public CategoryViewModel? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (value == _selectedCategory) return;

                _selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
                DeleteCategoryCommand.RaiseCanExecuteChanged();
                EditCategoryCommand.RaiseCanExecuteChanged();
                value?.Command.Execute(null);
            }
        }

        public async Task LoadMoreCategories(int countRecords = 15, int alreadyLoaded = 0)
        {
            var newCategories = await _categoryService.GetAsync(countRecords, alreadyLoaded);

            foreach (var category in newCategories)
                Categories.Add(CreateViewModel(category));
        }

        public async Task LoadMoreGoals(int countRecords = 15, int alreadyLoaded = 0)
        {
            var newGoals = await _goalService.GetAsync(countRecords, alreadyLoaded);

            foreach (var goal in newGoals)
                Goals.Add(CreateViewModel(goal));
        }

        /// <summary>
        /// Checks item in <see cref="Categories"/> for freshness.
        /// </summary>
        public async Task RefreshCategoriesAsync() =>
            await RefreshAsync(
                Categories,
                _categoryService,
                UpdateViewModel,
                CreateViewModel);

        /// <summary>
        /// Checks item in <see cref="Goals"/> for freshness.
        /// </summary>
        public async Task RefreshGoalsAsync() =>
            await RefreshAsync(
                Goals,
                _goalService,
                UpdateViewModel,
                CreateViewModel);

        /// <summary>
        /// Checks every item in <paramref name="collection"/> for freshness.
        /// Uses <paramref name="service"/> to get entities from database.
        /// Invokes <paramref name="updateViewModel"/> to update an exisiting entity view model.
        /// Invokes <paramref name="createViewModel"/> to create a new entity view model.
        /// </summary>
        public static async Task RefreshAsync<TViewModel, TEntity, TEnum>(
            ObservableCollection<TViewModel> collection,
            EntityService<TEntity, TEnum> service,
            Action<TViewModel, TEntity> updateViewModel,
            Func<TEntity, TViewModel> createViewModel
            ) 
            where TViewModel : EntityViewModel
            where TEntity : class, IEntity
            where TEnum: struct ,Enum

        {
            int count = collection.Count;

            var entities = await service.GetAsync(count);
            var ids = new HashSet<int>(entities.Select(s => s.Id));

            for (int i = count - 1; i >= 0; i--)
                if (!ids.Contains(collection[i].Id))
                    collection.RemoveAt(i);

            var existed = collection.ToDictionary(keySelector: d => d.Id);
            foreach (var entity in entities)
            {
                if (existed.TryGetValue(entity.Id, out var vm))
                    updateViewModel(vm, entity);
                else
                    collection.Add(createViewModel(entity));
            }
        }

        private CategoryViewModel CreateViewModel(Category entity)
        {
            CategoryViewModel category = new(entity, null);
            category.SetCommand(vm => new RelayCommand(() => Select(vm)));
            return category;
        }

        private GoalViewModel CreateViewModel(Goal entity)
        {
            GoalViewModel goal = new(entity, null);
            goal.SetCommand(vm => new RelayCommand(() => Select(vm)));
            return goal;
        }

        private void UpdateViewModel(CategoryViewModel viewModel, Category entity)
        {
            viewModel.Title = entity.Name;
        }

        private void UpdateViewModel(GoalViewModel viewModel, Goal entity)
        {
            viewModel.Title = entity.Name;
            viewModel.Description = entity.Description;
        }

        private void Select(EntityViewModel selected)
        {
            _mediator.Publish(new EntitySelect
            (
                entity: selected
            ));
        }
    }
}
