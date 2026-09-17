using Avalonia;
using CatchLightning.Core.Abstractions.Presentation;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows.Input;

namespace CatchLightning.features.Dashboard.NavigationPanel
{
    public class NavigationPath
    {
        public required string Name { get; init; }
        public required EntityViewModel Entity { get; init; }
        public required Depth LevelDepth { get; init; }
        public required AsyncRelayCommand Command { get; init; }

        public int Intent { get; set; } = 10;
        public Thickness Margin => new Thickness(Intent * (int)LevelDepth, 0, 0, 0);


        public enum Depth
        {
            Category = 0,
            Goal = 1,
            Achievements = 2
        }
    }
}
