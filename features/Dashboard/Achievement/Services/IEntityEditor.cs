using CatchLightning.Core.Abstractions.Presentation;
using CatchLightning.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchLightning.features.Dashboard.Achievement.Services
{
    public interface IEntityEditor<in TViewModel, TEnumErrors>
        where TViewModel: EntityViewModel
        where TEnumErrors : Enum 
    {
        Task<OperationResult<TEnumErrors>> CreateAsync(
            TViewModel viewModel,
            CancellationToken cancel = default);

        Task<OperationResult<TEnumErrors>> UpdateAsync(
            TViewModel viewModel,
            CancellationToken cancel = default);
    }
}
