namespace ruby_plotter.app.Contracts.Parameters;

/// <summary>
/// This class contains the parameters used to calculate the sinc function.
/// </summary>
public class SincParameter : ParameterBase
{
    public double xMin { get; set; }
    public double xMax { get; set; }
}
