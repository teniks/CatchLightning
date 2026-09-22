using System.Threading.Tasks;

namespace CatchLightning.Core.Abstractions
{
    public interface IValidator<TEntity, TEnumErrors>
    {
        Task<TEnumErrors?> Validate(TEntity entity);
    }
}
