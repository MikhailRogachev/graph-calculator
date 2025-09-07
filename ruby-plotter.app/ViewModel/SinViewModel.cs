using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// y = A * sin(x) + B
/// </summary>
public class SinViewModel : TrigonometricViewModel
{
    public SinViewModel(SinCosParameter parameter, TrigonometricValidationSettings validationSettings)
        : base(parameter, validationSettings) { }
}
