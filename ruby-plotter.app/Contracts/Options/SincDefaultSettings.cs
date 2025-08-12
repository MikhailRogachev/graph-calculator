namespace ruby_plotter.app.Contracts.Options;

/// <summary>
/// Represents the default settings for configuring Sinc function parameters.
/// This class provides default values for frequency and range parameters commonly used in Sinc function
/// calculations.
/// </summary>
public class SincDefaultSettings
{
    public double FrequencyMin { get; set; } = 0.0;
    public double FrequencyMax { get; set; } = 10.0;
    public double Xmax { get; set; } = 3.0;
    public double Xmin { get; set; } = -3.0;
}