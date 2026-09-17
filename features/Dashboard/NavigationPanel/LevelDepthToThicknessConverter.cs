using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CatchLightning.features.Dashboard.NavigationPanel
{
    public class LevelDepthToThicknessConverter : IValueConverter
    {
        public double Intent { get; set; } = 10.0;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Thickness(Intent * (int)value, 0, 0, 0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
