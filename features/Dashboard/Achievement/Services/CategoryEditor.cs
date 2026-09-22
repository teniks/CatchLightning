using CatchLightning.Core.Models;
using CatchLightning.Core.Services;
using CatchLightning.Core.Services.Validators;
using CatchLightning.features.Dashboard.Achievement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Achievement.Services
{
    public class CategoryEditor : IEntityEditor<CategoryViewModel, CategoryError>
    {
        private EntityService<Category, CategoryError> _service;


        public CategoryEditor(
            EntityService<Category, CategoryError> service)
        {
            _service = service;
        }

        public async Task<OperationResult<CategoryError>> CreateAsync(CategoryViewModel viewModel, CancellationToken cancel = default)
        {
            return await _service.AddAsync(new Category
            {
                Name = viewModel.Title
            });
        }

        public async Task<OperationResult<CategoryError>> UpdateAsync(CategoryViewModel viewModel, CancellationToken cancel = default)
        {
            return await _service.UpdateAsync(viewModel.Id, (category) => {
                category.Name = viewModel.Title;
            });
        }
    }
}
