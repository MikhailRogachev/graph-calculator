using ruby_plotter.app.Contracts.Validators;
using System.Globalization;

namespace ruby_plotter.tests.Contracts.Validators;

public class DoubleInputValidatorTests
{

    [Theory, MemberData(nameof(GetDataToValidate))]
    public void ValidDoubleInputTest(string input, bool isPositiveOnly, bool expectedValue)
    {
        // Arrange
        var validator = new DoubleInputValidator
        {
            IsPositiveOnly = isPositiveOnly
        };

        // Act
        var result = validator.Validate(input, CultureInfo.CurrentCulture);
        // Assert
        Assert.True(result.IsValid == expectedValue);
    }

    public static TheoryData<string, bool, bool> GetDataToValidate()
    {
        return new TheoryData<string, bool, bool>
        {
            { "123.45", true, true  },
            { string.Empty, true, false },
            { "", true, false  },
            { " ", true, false  },
            { "123,45", true, false  },
            { "1h3,45", true, false  },
            { "-123.45", true, false  },
            { "-123.45", false, true  },
            { "-12", false, true  },
            { "-12", true, false  },
            { "1", true, true  },
        };
    }
}
