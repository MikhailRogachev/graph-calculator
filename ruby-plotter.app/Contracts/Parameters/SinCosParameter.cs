namespace ruby_plotter.app.Contracts.Parameters;

/// <summary>
/// This class contains the parameters used to calculate the sine and cosine functions.
/// </summary>
public class SinCosParameter : ParameterBase
{
    public double Amplitude { get; set; }
    public int Phase { get; set; }
    public double Duration { get; set; }
}
