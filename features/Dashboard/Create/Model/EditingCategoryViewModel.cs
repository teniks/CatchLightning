using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Models;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using CatchLightning.features.Dashboard.Achievement.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Create.Model
{
    public class EditingCategoryViewModel : BaseEditungViewModel
    {
        private IEntityEditor<CategoryViewModel, CategoryError> _categoryEditor;

        protected override ObservableCollection<FieldViewModel> _availableFields { get; } = new();

        public EditingCategoryViewModel(
                IEntityEditor<CategoryViewModel, CategoryError> categoryEditor
            )
        {
            _categoryEditor = categoryEditor;
        }

        public void SetFieldsFor(CategoryViewModel category)
        {
            Id = category.Id;
            Title = category.Title;
        }

        public override async Task<OperationResult> CreateAsync()
        {
            return await _categoryEditor.CreateAsync(ToViewModel());
        }

        public override async Task<OperationResult> UpdateAsync()
        {
            return await _categoryEditor.UpdateAsync(ToViewModel());
        }

        private CategoryViewModel ToViewModel()
        {
            return new CategoryViewModel(new Category
            {
                Id = this.Id,
                Name = this.Title
            }, null);
        }
    }
}
