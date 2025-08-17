using System.Globalization;
using System.Windows.Controls;

namespace ruby_plotter.app.Services;

public class DoubleValidationRuleService : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        // Attempt to interpret the input value as a double.
        if (double.TryParse(value as string, out double result))
        {
            return ValidationResult.ValidResult;
        }
        // Return an error if the value does not constitute a valid double.
        return new ValidationResult(false, "The number format is invalid. Kindly input a valid decimal number.");
    }
}
