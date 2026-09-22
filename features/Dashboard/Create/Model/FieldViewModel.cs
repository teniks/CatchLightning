using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using CatchLightning.features.Dashboard.Achievement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CatchLightning.features.Dashboard.Create.Model
{
    public abstract class FieldViewModel(string name) : BaseViewModel
    {
        public string Title { get; } = name;

    }

    public sealed class DescriptionFieldViewModel() : FieldViewModel("Описание")
    {
        private string? _value;
        public string? Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }
    }

    public sealed class CategoryFieldViewModel() : FieldViewModel("Категория")
    {
        public ObservableCollection<CategoryViewModel> Options { get; set; } = new();

        private CategoryViewModel? _value;
        public CategoryViewModel? Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }
    }

    public sealed class GoalFieldViewModel() : FieldViewModel("Цель")
    {
        public ObservableCollection<GoalViewModel> Options { get; set; } = new();
        private GoalViewModel? _value;
        public GoalViewModel? Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }
    }

    public sealed class LevelFieldViewModel() : FieldViewModel("Уровень достижения")
    {
        public IReadOnlyList<AchievementLevel> Options { get; } =
            Enum.GetValues<AchievementLevel>();
        private AchievementLevel _value;
        public AchievementLevel Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }
    }

    public sealed class CompletionStatusFieldViewModel() : FieldViewModel("Статус выполнения")
    {
        private bool _value;
        public bool Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }
    }
}
