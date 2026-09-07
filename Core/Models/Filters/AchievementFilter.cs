using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning.Core.Models.Filters
{
    internal class AchievementFilter
    {
        public int? CategoryId { get; set; }
        public List<int>? GoalIds { get; set; } = null;
        public bool? IsCompleted { get; set; }

    }
}
