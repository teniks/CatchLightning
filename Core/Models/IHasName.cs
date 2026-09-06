using System;
using System.Collections.Generic;
using System.Text;

namespace CatchLightning.Core.Models
{
    /// <summary>
    /// Every model has a name property.
    /// </summary>
    internal interface IHasName
    {
        public string Name { get; set; }
    }
}
