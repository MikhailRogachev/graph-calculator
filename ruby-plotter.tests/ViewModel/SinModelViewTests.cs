using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;
using ruby_plotter.tests.Utilites.Helpers;
using UnitTests.Utility.Attributes;
using UnitTests.Utility.Kernel;

namespace ruby_plotter.tests.ViewModel;

public class SinModelViewTests
{
    public static SinCosParameter GetParameter() => TestHelper.GetSinCosParameter();
    public static TrigonometricValidationSettings GetSettings() => TestHelper.GetTrigonometricValidationSettings();
    public static double GetRandomDouble(double minValue = -1, double maxValue = 2) =>
        TestHelper.GenerateDoubleValue(minValue, maxValue, new double[] { 0, 1, 10 });

    [Theory, AutoMock]
    public void CreateViewTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings
        )
    {
        var model = new SinViewModel(parameter, settings);

        Assert.Equal(parameter.Phase, model.Phase);
        Assert.Equal(parameter.Frequency, model.Frequency);
        Assert.Equal(parameter.Amplitude, model.Amplitude);
        Assert.Equal(parameter.Duration, model.Duration);
    }

    #region Change parameters and validation

    [Theory, AutoMock]
    public void AmplitudeUpdatedValidTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
        [RegInstance(nameof(GetRandomDouble), new object[] { 1, 10 })] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Amplitude = value;

        // assert
        Assert.False(model.HasErrors);
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Amplitude))));
    }

    [Theory, AutoMock]
    public void AmplitudeUpdatedNegatInvalidTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
        [RegInstance(nameof(GetRandomDouble), new object[] { -10, -1 })] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Amplitude = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Amplitude))));
    }

    [Theory, AutoMock]
    public void AmplitudeUpdatedPositInvalidTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
        [RegInstance(nameof(GetRandomDouble), new object[] { 11, 25 })] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Amplitude = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Amplitude))));
    }

    [Theory, AutoMock]
    public void DurationUpdatedTest(
        [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
        [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
        [RegInstance(nameof(GetRandomDouble), new object[] { 0, 10 })] double value,
        ModelViewTester tester
        )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Duration = value;

        // assert
        Assert.False(model.HasErrors);
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Duration))));
    }

    [Theory, AutoMock]
    public void DurationUpdatedPositInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { 20, 100 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Duration = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Duration))));
    }

    [Theory, AutoMock]
    public void DurationUpdatedNegativInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { -20, -1 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Duration = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Duration))));
    }

    [Theory, AutoMock]
    public void PhaseUpdatedTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { -20, 20 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Phase = value;

        // assert
        Assert.False(model.HasErrors);
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Phase))));
    }

    [Theory, AutoMock]
    public void PhaseUpdatedPositivInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { 70, 120 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Phase = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Phase))));
    }
    [Theory, AutoMock]
    public void PhaseUpdatedNegatInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { -170, -120 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Phase = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Phase))));
    }

    [Theory, AutoMock]
    public void FrequencyUpdatedTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { 1, 20 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Frequency = value;

        // assert
        Assert.False(model.HasErrors);
        Assert.Equal(1, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Frequency))));
    }

    [Theory, AutoMock]
    public void FrequencyUpdatedNegatInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { -1, 0 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Frequency = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Frequency))));
    }

    [Theory, AutoMock]
    public void FrequencyUpdatedPositInvalidTest(
       [RegInstance(nameof(GetParameter))] SinCosParameter parameter,
       [RegInstance(nameof(GetSettings))] TrigonometricValidationSettings settings,
       [RegInstance(nameof(GetRandomDouble), new object[] { 100000, 2000000 })] double value,
       ModelViewTester tester
       )
    {
        // arrange
        var model = new SinViewModel(parameter, settings);
        model.PropertyChanged += tester.Model_PropertyChanged;

        // act
        model.Frequency = value;

        // assert
        Assert.True(model.HasErrors);
        Assert.Equal(2, tester.PropertiesChanged?.ToList().Count);
        Assert.True(tester.PropertiesChanged?.Any(p => p.Equals(nameof(model.Frequency))));
    }

    #endregion

}
