namespace ruby_plotter.app.Contracts.Options;

public class AppDefaultSettings
{
    public SinDefaultSettings SinDefaultSettings { get; set; } = new SinDefaultSettings();
    public CosDefaultSettings CosDefaultSettings { get; set; } = new CosDefaultSettings();
    public SincDefaultSettings SincDefaultSettings { get; set; } = new SincDefaultSettings();
    public PloterDefaultSettings PloterDefaultSettings { get; set; } = new PloterDefaultSettings();
    public string? DefaultFilePath { get; set; } = null;
}
