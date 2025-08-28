namespace ruby_plotter.app.Contracts.Extensions;

/// <summary>
/// This class contains functions and methods to calculate sine, cosine, 
/// and sinc functions. Sine and cosine are calculated by the same function 
/// because the only difference is a phase shift of 90 degrees.
/// </summary>
public static class FuncGenerator
{
    /// <summary>
    /// This function calculates sine or cosine and returns the result as two arrays: 
    /// one for time stamps and another for amplitude values.
    /// </summary>
    /// <remarks>The method calculates the sine wave using the formula: <c>y(t) = amplitude * sin(2π *
    /// frequency * t + phase)</c>, where <c>t</c> is the time in seconds. The frequency is converted from kHz to Hz,
    /// and the phase is converted from degrees to radians.</remarks>
    /// <param name="frequency">The frequency of the sine wave in Hz. Defaults to50 Hz.</param>
    /// <param name="duration">The duration of the sine wave in seconds. Defaults to 1 second.</param>
    /// <param name="amplitude">The amplitude of the sine wave. Defaults to 1.</param>
    /// <param name="phase">The phase offset of the sine wave in degrees. Defaults to 0 degrees.</param>
    /// <param name="framerate">The sampling rate in samples per second. Defaults to 24,600 samples per second.</param>
    /// <returns>A tuple containing two arrays: <list type="bullet"> <item><description><c>Ys</c>: The generated sine wave
    /// values.</description></item> <item><description><c>Ts</c>: The corresponding time values for each
    /// sample.</description></item> </list></returns>
    public static (double[] Ys, double[] Ts) Sin(
        double frequency = 50,
        double duration = 1,
        double amplitude = 1,
        double phase = 0,
        double framerate = 24600)
    {
        var freqKhz = frequency;
        int count = Convert.ToInt32(duration * framerate);

        double[] ts = new double[count];
        double[] ys = new double[count];

        for (int i = 0; i < count; i++)
        {
            ts[i] = i / framerate;
            ys[i] = amplitude * Math.Sin(0.5 * Math.PI * freqKhz * ts[i] + phase);
        }

        return (ys, ts);
    }

    /// <summary>
    /// Generates the values of the sinc (sinus cardinalis) function over a specified range and frequency.
    /// </summary>
    /// <remarks>The sinc function is defined as <c>sin(π * frequency * t) / (π * frequency * t)</c>, where
    /// <c>t</c> is the time. If <c>t</c> is 0, the function value is defined as 1 to avoid division by zero.</remarks>
    /// <param name="frequency">The frequency of the sinc function in Hz. Defaults to 0.05 kHz.</param>
    /// <param name="xStart">The starting value of the range for the independent variable (time). Defaults to 0.</param>
    /// <param name="xEnd">The ending value of the range for the independent variable (time). Defaults to 1.</param>
    /// <param name="framerate">The number of samples per second used to calculate the function. Defaults to 24600.</param>
    /// <returns>A tuple containing two arrays: <list type="bullet"> <item><description><c>Ys</c>: The calculated values of the
    /// sinc function.</description></item> <item><description><c>Ts</c>: The corresponding time values for the
    /// calculated sinc function values.</description></item> </list></returns>
    public static (double[] Ys, double[] Ts) SinCardinal(
        double frequency = 20,
        double xStart = 0,
        double xEnd = 1,
        double framerate = 24600)
    {
        var freqKhz = frequency;
        var duration = xEnd + (xStart < 0 ? xStart * -1 : xStart);
        int count = Convert.ToInt32(duration * framerate);

        double[] ts = new double[count];
        double[] ys = new double[count];


        for (int i = 0; i < count; i++)
        {
            ts[i] = xStart + i / framerate;

            if (ts[i] == 0)
            {
                ys[i] = 1;
            }
            else
            {
                ys[i] = Math.Sin(Math.PI * freqKhz * ts[i]) / (Math.PI * freqKhz * ts[i]);
            }
        }

        return (ys, ts);
    }
}