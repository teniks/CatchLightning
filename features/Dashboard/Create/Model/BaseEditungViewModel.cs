using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Create.Model
{
    public abstract class BaseEditungViewModel : BaseViewModel
    {
        protected int Id { get; set; } = -2;
        public string Title
        {
            get => field;
            set
            {
                field = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public ObservableCollection<FieldViewModel> VisibleFields { get; set; } = new();
        protected abstract ObservableCollection<FieldViewModel> _availableFields { get; }
        public ObservableCollection<FieldViewModel> AvailableFields { get; } = new();
        protected readonly Dictionary<Type, FieldViewModel> _byType = new();


        public BaseEditungViewModel()
        {
            Title = string.Empty;

            foreach (var field in _availableFields)
            {
                AvailableFields.Add(field);
                _byType[field.GetType()] = field;
            }
        }

        protected TField GetField<TField>() where TField : FieldViewModel =>
            (TField)_byType[typeof(TField)];

        public abstract Task<OperationResult> CreateAsync();
        public abstract Task<OperationResult> UpdateAsync();
    }
}
