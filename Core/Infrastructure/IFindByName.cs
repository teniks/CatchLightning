using CatchLightning.Core.Models;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    /// <summary>
    /// Provides a method to find an entity by its name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IFindByName<TEntity> where TEntity : class, IHasName
    {
        Task<TEntity?> GetByNameAsync(string name);
    }
}
