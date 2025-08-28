using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;
using UnitTests.Utility.Attributes;

namespace ruby_plotter.tests.ViewModel;

public class SinModelViewTests
{

    [Theory, AutoMock]
    public void CreateViewTest(SinCosParameter parameter)
    {
        var model = new SinViewModel(parameter);

        Assert.Equal(parameter.Phase, model.Phase);
        Assert.Equal(parameter.Frequency, model.Frequency);
        Assert.Equal(parameter.Amplitude, model.Amplitude);
        Assert.Equal(parameter.Duration, model.Duration);
    }
}
