using ruby_plotter.app.Contracts.Enums;
using System.Globalization;
using System.Windows.Data;

namespace ruby_plotter.app.Contracts.Converters;

/// <summary>
/// Converts a <see cref="VisibleMode"/> enumeration value to its corresponding integer representation.
/// This converter is used in data binding scenarios where a numeric value is required to represent a
/// visibility mode.
/// </summary>
public class ControlVisibleModeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var visibleValue = (VisibleMode)value;
        return (int)visibleValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
