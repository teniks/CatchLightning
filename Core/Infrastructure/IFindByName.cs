using CatchLightning.Core.Models;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    /// <summary>
    /// Provides a method to find an entity by its name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IFindByName<T> where T : class, IHasName
    {
         Task<T?> GetByNameAsync(string name);
    }
}
