namespace ruby_plotter.tests.Utilites.Helpers;

public static class TestHelper
{
    public static double GetDoubleExcludeRangeValue(double[] excludeRange)
    {
        var _random = new Random();
        double result;
        do
        {
            var value = _random.NextInt64(20);
            result = (double)value;
        } while (excludeRange.Contains(result));

        return result;
    }
}
