using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    public interface IValidator<TEntity, TEnumErrors>
    {
        Task<TEnumErrors?> Validate(TEntity entity);
    }
}
