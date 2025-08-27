namespace ruby_plotter.app.Contracts.Parameters;

/// <summary>
/// This class is the base class for parameters used to calculate sine, cosine, and sinc functions.
/// </summary>
public class ParameterBase
{
    public double Frequency { get; set; }
    public int FrequencyMeasureId { get; set; }
}
