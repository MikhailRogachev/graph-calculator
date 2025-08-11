using System.Globalization;
using System.Windows.Data;

namespace ruby_plotter.app.Contracts.Converters;

/// <summary>
/// Provides methods to convert between a <see langword="double"/> value and its textual representation.
/// </summary>
public class CustomTextDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return double.TryParse(value.ToString(), out var result) ? result : 0.0;
    }
}
