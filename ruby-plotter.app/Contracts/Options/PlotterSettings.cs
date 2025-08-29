namespace ruby_plotter.app.Contracts.Options;

public class PlotterSettings
{
    public double XMin { get; set; }
    public double XMax { get; set; }
    public double YMin { get; set; }
    public double YMax { get; set; }
    public string Xlabel { get; set; } = string.Empty;
    public string Ylabel { get; set; } = string.Empty;
    public float LineWidth { get; set; }
    public double Framerate { get; set; }
}
