using System.Collections.Generic;

namespace CatchLightning.Core.Models
{
    internal class Goal: IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? IdCategory { get; set; }
        public Category? Category { get; set; }

        public List<Achievement> Achivments { get; } = new();
    }
}
