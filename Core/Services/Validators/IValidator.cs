using System;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    internal interface IValidator<TEntity, TEnumErrors>
    {
        Task<TEnumErrors?> Validate(TEntity entity);
    }
}
