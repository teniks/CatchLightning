
using CatchLightning.Core.Abstractions;

namespace CatchLightning.Core.Models
{
    public class Category : IEntity
    {
        public int Id { get; init; }
        public required string Name { get; set; }
    }
}
