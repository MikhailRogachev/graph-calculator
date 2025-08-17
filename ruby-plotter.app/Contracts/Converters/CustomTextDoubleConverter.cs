using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ruby_plotter.app.Contracts.Converters;

/// <summary>
/// Provides methods to convert between a <see langword="double"/> value and its textual representation.
/// </summary>
public class CustomTextDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
            return doubleValue.ToString("0.00");

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is string s)
        {
            if (string.IsNullOrEmpty(s) || s == ".")
                return "0.0";
            else if (s.EndsWith("."))
                s += "0";

            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                return result;
        }

        return DependencyProperty.UnsetValue;
    }
}
