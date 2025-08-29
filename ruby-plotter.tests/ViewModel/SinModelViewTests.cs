using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;
using ruby_plotter.tests.Utilites.Helpers;
using UnitTests.Utility.Attributes;
using UnitTests.Utility.Kernel;

namespace ruby_plotter.tests.ViewModel;

public class SinModelViewTests
{
    public static SinCosParameter GetParameter() => new SinCosParameter()
    {
        Amplitude = 1,
        Duration = 10,
        Frequency = 10,
        FrequencyMeasureId = 1,
        Phase = 0,
        PhaseMeasureId = 1
    };

    public static double GetRandomDouble() =>
        TestHelper.GetDoubleExcludeRangeValue(new double[] { 0, 1, 10 });

    [Theory, AutoMock]
    public void CreateViewTest(SinCosParameter parameter)
    {
        var model = new SinViewModel(parameter);

        Assert.Equal(parameter.Phase, model.Phase);
        Assert.Equal(parameter.Frequency, model.Frequency);
        Assert.Equal(parameter.Amplitude, model.Amplitude);
        Assert.Equal(parameter.Duration, model.Duration);
    }

    [Theory, AutoMock]
    public void AmplitudeUpdatedTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetRandomDouble))] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Amplitude = value;

        // assert
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Amplitude))));
    }

    [Theory, AutoMock]
    public void AmplitudeNotUpdatedTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Amplitude = parameter.Amplitude;

        // assert
        Assert.True(!tester.PropertiesChanged.Any());
    }

    [Theory, AutoMock]
    public void DurationUpdatedTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetRandomDouble))] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Duration = value;

        // assert
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Duration))));
    }

    [Theory, AutoMock]
    public void DurationNotUpdatedTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Duration = parameter.Duration;

        // assert
        Assert.True(!tester.PropertiesChanged.Any());
    }
}
