namespace ruby_plotter.app.Contracts.Options;

public class TrigonometricValidationSettings : ValidationSettings
{
    public double AmplitudeMin { get; set; }
    public double AmplitudeMax { get; set; }
    public double AmplitudeDefault { get; set; }
    public double DurationMax { get; set; }
    public double DurationMin { get; set; }
    public double PhaseMin { get; set; }
    public double PhaseMax { get; set; }
}
