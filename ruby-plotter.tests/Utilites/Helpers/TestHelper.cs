using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.tests.Utilites.Helpers;

public static class TestHelper
{
    public static double GenerateDoubleValue(double minValue = -100, double maxValue = 10000, double[]? excludeRange = null)
    {
        if (excludeRange == null || excludeRange.Length == 0)
        {
            return GetDoubleInRange(minValue, maxValue);
        }

        return GetDoubleExcludeRangeValue(minValue, maxValue, excludeRange);
    }

    public static double GetDoubleExcludeRangeValue(double minValue, double maxValue, double[] excludeRange)
    {
        var _random = new Random();
        double result;
        do
        {
            var value = _random.NextInt64((long)minValue, (long)maxValue);
            result = (double)value;
        } while (excludeRange.Contains(result));

        return result;
    }

    public static double GetDoubleInRange(double minValue, double maxValue)
    {
        var _random = new Random();

        var value = _random.NextInt64((long)minValue, (long)maxValue);
        return (double)value;
    }

    public static SinCosParameter GetSinCosParameter() => new SinCosParameter()
    {
        Amplitude = 1,
        Duration = 10,
        Frequency = 10,
        FrequencyMeasureId = 1,
        Phase = 0,
        PhaseMeasureId = 1
    };

    public static TrigonometricValidationSettings GetTrigonometricValidationSettings()
    {
        return new TrigonometricValidationSettings()
        {
            AmplitudeMin = 0,
            AmplitudeMax = 10,
            DurationMin = 1,
            DurationMax = 20,
            FrequencyHzMin = 1,
            FrequencyHzMax = 10000,
            PhaseMin = -30,
            PhaseMax = 30
        };
    }
}
