namespace ruby_plotter.app.Contracts.Options;

/// <summary>
/// Represents the default application settings for various mathematical operations and plotting configurations.
/// This class provides default settings for sine, cosine, and sinc operations, as well as plotting
/// configurations. It also includes an optional default file path for saving or loading configuration data.
/// </summary>
public class AppDefaultSettings
{
    public TrigonometricPlotterSettings TrigonometricPlotterSettings { get; set; } = new TrigonometricPlotterSettings();
    public SinCardinalPlotterSettings SinCardinalPlotterSettings { get; set; } = new SinCardinalPlotterSettings();
    public TrigonometricValidationSettings TrigonometricValidationSettings { get; set; } = new TrigonometricValidationSettings();
    public SincValidationSettings SincValidationSettings { get; set; } = new SincValidationSettings();

    /// <summary>
    /// Gets or sets the default file path used by the application.
    /// This parameter defines where the application will save or load
    /// sine, cosine, and sinc operations results.
    /// </summary>
    public string? DefaultFilePath { get; set; } = null;
}
