using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// This class represents the ViewModel for the Cosine wave parameters.
/// </summary>
public class CosViewModel : TrigonometricViewModel
{
    public CosViewModel(SinCosParameter parameter) : base(parameter) { }
}
