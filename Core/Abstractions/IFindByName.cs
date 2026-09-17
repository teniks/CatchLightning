using System.Threading.Tasks;

namespace CatchLightning.Core.Abstractions
{
    /// <summary>
    /// Provides a method to find an entity by its name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IFindByName<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByNameAsync(string name);
    }
}
