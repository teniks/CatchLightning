using System;
using System.Threading.Tasks;

namespace CatchLightning.Core.Abstractions
{
    public interface IGetterById<TEntity>
    {
        Task<TEntity?> GetByIdAsync(int id);
    }
}
