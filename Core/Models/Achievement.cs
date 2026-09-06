using System;
using System.Collections.Generic;

namespace CatchLightning.Core.Models
{
    internal class Achievement: IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public AchievementLevel Level { get; set; }
        public bool IsCompleted { get; set; } 
        public DateTime? DateCompleted { get; set; }

        public int? GoalId { get; set; }
        public Goal? Goal { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Achievement> Conditions { get; } = new ();
        public List<Achievement> Dependents { get; } = new ();
        
    }
}
