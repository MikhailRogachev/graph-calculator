using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ruby_plotter.app.Contracts.Converters;

/// <summary>
/// Provides methods to convert between a <see langword="int"/> value and its textual representation.
/// </summary>
public class CustomTextIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int intValue)
            return intValue.ToString("0");

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string s)
        {
            if (string.IsNullOrEmpty(s))
                return DependencyProperty.UnsetValue;

            if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
                return result;
        }

        return DependencyProperty.UnsetValue;
    }
}
