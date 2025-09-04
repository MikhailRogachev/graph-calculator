using System.Globalization;
using System.Windows.Controls;

namespace ruby_plotter.app.Contracts.Validators;

/// <summary>
/// This class represents a validation rule that checks whether a given input is a valid double-precision number, 
/// with an optional restriction to positive values only.
/// </summary>
/// <remarks>
///     This validation rule is typically used in data binding scenarios to validate user input. 
///     The rule ensures that the input is a valid numeric value and, if <see cref="IsPositiveOnly"/> 
///     is set to <see langword="true"/>, that the value is non-negative.</remarks>
public class DoubleInputValidator : ValidationRule
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation is restricted to positive values only.
    /// </summary>
    public bool IsPositiveOnly { get; set; }

    /// <summary>
    /// Validates the specified input value to ensure it meets the required criteria.
    /// </summary>
    /// <param name="value">
    ///     The value to validate. Must be a non-empty string representing a valid number.
    /// </param>
    /// <param name="cultureInfo">
    ///     The culture-specific formatting information to use during validation.
    /// </param>
    /// <returns>
    ///     A <see cref="ValidationResult"/> indicating whether the input is valid.  
    ///     Returns a valid result if the input is a non-empty string representing 
    ///     a valid number,  and, if <see cref="IsPositiveOnly"/> is <see langword="true"/>,
    ///     the number is positive.
    /// </returns>
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is not string input || string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "Input cannot be empty.");
        }

        if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double _value))
        {
            return new ValidationResult(false, "Input must be a valid number.");
        }

        if (IsPositiveOnly && _value < 0)
        {
            return new ValidationResult(false, "Input must be a positive number.");
        }

        return ValidationResult.ValidResult;
    }
}
