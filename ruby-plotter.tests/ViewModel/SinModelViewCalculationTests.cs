using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;
using ruby_plotter.tests.Utilites.Helpers;
using UnitTests.Utility.Attributes;

namespace ruby_plotter.tests.ViewModel;

public class SinModelViewCalculationTests
{
    public static SinCosParameter GetParameter() => TestHelper.GetSinCosParameter();
    public static TrigonometricValidationSettings GetSettings()
        => TestHelper.GetTrigonometricValidationSettings();


    [Theory, AutoMock]
    public void GradToRadCalculationTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings
        )
    {
        // Arrange
        var value = 90;
        var valueRad = 90 * Math.PI / 180;
        var model = new SinViewModel(parameter, settings);
        model.Phase = value;

        // Act
        var radMeasure = model.PhaseMeasures.First(p => p.Id == 2);
        model.SelectedPhaseMeasure = radMeasure;

        // Assert
        Assert.Equal(valueRad, model.Phase);
    }

}
