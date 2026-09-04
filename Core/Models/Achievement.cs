using System.Collections.Generic;

namespace CatchLightning.Core.Models
{
    internal class Achievement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public AchievementLevel Level { get; set; }
        public bool IsComplited { get; set; } 
        public string? DateComplited { get; set; }

        public int? IdGoal { get; set; }
        public Goal? Goal { get; set; }

        public int? IdCategory { get; set; }
        public Category? Category { get; set; }

        public List<Achievement> Сonditions { get; } = new ();
        public List<Achievement> Dependents { get; } = new();
        
    }
}
