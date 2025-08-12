namespace ruby_plotter.app.Contracts.Options;

/// <summary>
/// Represents the default configuration settings for generating sine wave parameters.
/// This class provides default minimum and maximum values for various properties of a sine wave, 
/// including amplitude, frequency, duration, and phase.
/// </summary>
public class SinDefaultSettings
{
    public double AmplitudeMin { get; set; } = -2.5;
    public double AmplitudeMax { get; set; } = 2.5;
    public double FrequencyMin { get; set; } = 0.0;
    public double FrequencyMax { get; set; } = 10.0;
    public double DurationMax { get; set; } = 3.0;
    public double DurationMin { get; set; } = 0.0;
    public int PhaseMin { get; set; } = -180;
    public int PhaseMax { get; set; } = 180;
}