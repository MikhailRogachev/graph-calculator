using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;

namespace ruby_plotter.tests.ViewModel;

public class CosViewModelPhaseTests
{
    [Theory, MemberData(nameof(GetTestData))]
    public void PhaseChangesTest(CosViewModel model, int selectedPhaseId, double expectedPhase)
    {
        model.SelectedPhaseMeasure = model.PhaseMeasures.First(p => p.Id == selectedPhaseId);
        Assert.True(model.Phase == expectedPhase);
    }

    public static TheoryData<CosViewModel, int, double> GetTestData()
    {
        return new TheoryData<CosViewModel, int, double>
        {
            {
                new CosViewModel(
                    new SinCosParameter(){
                        Amplitude = 1,
                        Duration = 1,
                        Frequency = 50,
                        FrequencyMeasureId = 1,
                        Phase = 90,
                        PhaseMeasureId = 1
                    },
                    new CosDefaultSettings()),
                2,
                90 * Math.PI / 180
            },
            {
                new CosViewModel(
                    new SinCosParameter(){
                        Amplitude = 1,
                        Duration = 1,
                        Frequency = 50,
                        FrequencyMeasureId = 1,
                        Phase = 90,
                        PhaseMeasureId = 2
                    },
                    new CosDefaultSettings()),
                1,
                90
            }
        };

    }


}
