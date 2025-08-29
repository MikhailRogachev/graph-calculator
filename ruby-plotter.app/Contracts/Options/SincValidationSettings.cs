namespace ruby_plotter.app.Contracts.Options;

public class SincValidationSettings : ValidationSettings
{
    public double XMax { get; set; } = 3.0;
    public double XMin { get; set; } = -3.0;
}
