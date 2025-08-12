namespace ruby_plotter.app.Contracts.Options;

/// <summary>
/// Represents the default application settings for various mathematical operations and plotting configurations.
/// This class provides default settings for sine, cosine, and sinc operations, as well as plotting
/// configurations. It also includes an optional default file path for saving or loading configuration data.
/// </summary>
public class AppDefaultSettings
{
    public SinDefaultSettings SinDefaultSettings { get; set; } = new SinDefaultSettings();
    public CosDefaultSettings CosDefaultSettings { get; set; } = new CosDefaultSettings();
    public SincDefaultSettings SincDefaultSettings { get; set; } = new SincDefaultSettings();
    public PloterDefaultSettings PloterDefaultSettings { get; set; } = new PloterDefaultSettings();

    /// <summary>
    /// Gets or sets the default file path used by the application.
    /// This parameter defines where the application will save or load
    /// sine, cosine, and sinc operations results.
    /// </summary>
    public string? DefaultFilePath { get; set; } = null;
}
