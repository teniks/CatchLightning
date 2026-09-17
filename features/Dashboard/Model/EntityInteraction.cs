using CatchLightning.Core.Abstractions.Presentation;
using System;

namespace CatchLightning.features.Dashboard.Model
{
    internal sealed record EntitySelect(EntityViewModel entity);
    internal sealed record EntityRevoke(EntityViewModel entity);
}
