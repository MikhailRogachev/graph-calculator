namespace ruby_plotter.app.Contracts.Options;

/// <summary>
/// Represents the default settings for a plotter, including axis ranges, labels, line width, and frame rate.
/// This class provides a set of default values for configuring a plotter's appearance and behavior. 
/// </summary>
public class PloterDefaultSettings
{
    public double Xmin { get; set; } = -2;
    public double Xmax { get; set; } = 3;
    public double Ymin { get; set; } = -3;
    public double Ymax { get; set; } = 3;
    public string Xlabel { get; set; } = "Time (sec)";
    public string Ylabel { get; set; } = "Amplitude";
    public float LineWidth { get; set; } = 2.0f;
    public double Framerate { get; set; } = 28600.0;
}
