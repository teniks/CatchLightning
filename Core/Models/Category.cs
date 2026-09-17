
using CatchLightning.Core.Abstractions;

namespace CatchLightning.Core.Models
{
    public class Category : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
    }
}
