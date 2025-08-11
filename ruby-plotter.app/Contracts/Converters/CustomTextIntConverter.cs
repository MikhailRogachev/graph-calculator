using System.Globalization;
using System.Windows.Data;

namespace ruby_plotter.app.Contracts.Converters;

/// <summary>
/// Provides methods to convert between a <see langword="int"/> value and its textual representation.
/// </summary>
public class CustomTextIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return int.TryParse(value.ToString(), out var result) ? result : 0;
    }
}
