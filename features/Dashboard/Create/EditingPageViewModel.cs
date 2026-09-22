using Avalonia;
using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Services;
using CatchLightning.features.Dashboard.Achievement.Model;
using CatchLightning.features.Dashboard.Create.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.Create
{
    public class EditingPageViewModel : BaseViewModel
    {
        private Mode CurrentMode
        {
            get => field;
            set
            {
                field = value;
                RaisePropertyChanged(nameof(CurrentMode));
            }
        }
        public string CurrentModeTitle => CurrentPage.ToDisplay(CurrentMode);

        public EntityPage CurrentPage
        {
            get => field;
            set
            {
                field = value;
                RaisePropertyChanged(nameof(CurrentPage));
                RaisePropertyChanged(nameof(IsCategoryPageVisible));
                RaisePropertyChanged(nameof(IsGoalPageVisible));
                RaisePropertyChanged(nameof(IsAchievementVisible));

                RaisePropertyChanged(nameof(AvailableFieldToPick));
                RaisePropertyChanged(nameof(CurrentPageEditor));
                RaisePropertyChanged(nameof(CurrentModeTitle));
            }
        }

        public BaseEditungViewModel? CurrentPageEditor => CurrentPage switch 
        {
            EntityPage.Category     => _editingCategory,
            EntityPage.Goal         => _editingGoal,
            EntityPage.Achievement  => _editingAchievement,
            _                       => null
        };

        public bool IsCategoryPageVisible =>
            CurrentMode is Mode.Create || CurrentPage is EntityPage.Category;
        public bool IsGoalPageVisible =>
            CurrentMode is Mode.Create || CurrentPage is EntityPage.Goal;
        public bool IsAchievementVisible =>
            CurrentMode is Mode.Create || CurrentPage is EntityPage.Achievement;

        public EditingCategoryViewModel _editingCategory;
        public EditingGoalViewModel _editingGoal;
        public EditingAchievementViewModel _editingAchievement;


        public FieldViewModel? _selectedField;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddFiledCommand { get; }

        public IEnumerable<FieldViewModel> AvailableFieldToPick => CurrentPage switch
        {
            EntityPage.Goal => _editingGoal.AvailableFields.Where(w => !_editingGoal.VisibleFields.Contains(w)),
            EntityPage.Achievement => _editingAchievement.AvailableFields.Where(w => !_editingAchievement.VisibleFields.Contains(w)),
            _ => []
        };
            
        public FieldViewModel? SelectedField
        {
            get => _selectedField;
            set
            {
                _selectedField = value;
                RaisePropertyChanged(nameof(SelectedField));
            }
        }

        public EditingPageViewModel(
            EditingCategoryViewModel editingCategory,
            EditingGoalViewModel editingGoal,
            EditingAchievementViewModel editingAchievement,
            ICommand onCancel,
            EntityViewModel? selectedViewModel = null
            )
        {
            _editingCategory = editingCategory;
            _editingGoal = editingGoal;
            _editingAchievement = editingAchievement;

            AddFiledCommand = new RelayCommand(AddField);
            CancelCommand = onCancel;
            SaveCommand = new AsyncRelayCommand(
                execute:        ct  => SaveAsync(),
                canExecute:     ()  => CurrentPageEditor is not null,
                onException:    ex  => {
                    //TODO: Implement Error display. ViewModel provides operation state, UI displays it.
                    });

                    PropertyChanged += OnCurrentPage_Changed;
            _ = LoadAsync(selectedViewModel);
        }

        public async Task LoadAsync(EntityViewModel? selectedViewModel = null)
        {
            if (selectedViewModel is null)
            {
                CurrentMode = Mode.Create;
                return;
            }

            switch (selectedViewModel)
            {
                case CategoryViewModel category:
                    CurrentMode = Mode.Edit;
                    CurrentPage = EntityPage.Category;
                    _editingCategory.SetFieldsFor(category);
                    break;

                case GoalViewModel goal:
                    CurrentMode = Mode.Edit;
                    CurrentPage = EntityPage.Goal;
                    _editingGoal.SetFieldsFor(goal);
                    break;

                case AchievementViewModel achievement:
                    CurrentMode = Mode.Edit;
                    CurrentPage = EntityPage.Achievement;
                    _editingAchievement.SetFieldsFor(achievement);
                    break;
                default:
                    throw new NotSupportedException($"There is not has Editor for {selectedViewModel.GetType().Name}");
            }
        }

        private void OnCurrentPage_Changed(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName is not nameof(CurrentPage)) return;

            
        }

        private void AddField()
        {
            if (SelectedField is null) return;

            switch(CurrentPage)
            {
                case EntityPage.Goal:
                    if (!_editingGoal.VisibleFields.Contains(SelectedField))
                        _editingGoal.VisibleFields.Add(SelectedField);
                    break;

                case EntityPage.Achievement:
                    if (!_editingAchievement.VisibleFields.Contains(SelectedField))
                        _editingAchievement.VisibleFields.Add(SelectedField);
                    break;

                default: 
                    break;
            }


            RaisePropertyChanged(nameof(AvailableFieldToPick));
            SelectedField = null;
        }

        private async Task SaveAsync()
        {
            if (CurrentPageEditor is null)
                return;

            _ = CurrentMode switch
            {
                Mode.Create     => await CurrentPageEditor.CreateAsync(),
                Mode.Edit       => await CurrentPageEditor.UpdateAsync(),
                _               => OperationResult.Success()
            };
        }

        public enum Mode
        {
            Create,
            Edit
        }

        public enum EntityPage
        {
            Category,
            Goal,
            Achievement,
        }
    }
}
