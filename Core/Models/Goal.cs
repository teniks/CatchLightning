using CatchLightning.Core.Abstractions;
using System.Collections.Generic;

namespace CatchLightning.Core.Models
{
    public class Goal : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Achievement> Achievements { get; } = new();
    }
}
