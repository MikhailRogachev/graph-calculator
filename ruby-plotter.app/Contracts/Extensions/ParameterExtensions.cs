using ruby_plotter.app.Contracts.Parameters;
using ruby_plotter.app.ViewModel;

namespace ruby_plotter.app.Contracts.Extensions;

public static class ParameterExtensions
{
    public static SinCosParameter GetDefaultParameter()
    {
        return new SinCosParameter
        {
            Frequency = 10,
            FrequencyMeasureId = 1,
            Phase = 0,
            PhaseMeasureId = 1,
            Amplitude = 1,
            Duration = 2.0
        };
    }

    public static SincParameter GetDefaultSincParameter()
    {
        return new SincParameter
        {
            Frequency = 10,
            FrequencyMeasureId = 1,
            xMax = 2.0,
            xMin = -2.0
        };
    }

    public static SinCosParameter GetParameter(this SinViewModel model)
    {
        return new SinCosParameter
        {
            Frequency = model.Frequency,
            FrequencyMeasureId = model.SelectedFrequencyMeasure.Id,
            Phase = model.Phase,
            PhaseMeasureId = model.SelectedPhaseMeasure.Id,
            Amplitude = model.Amplitude,
            Duration = model.Duration
        };
    }

    public static SinCosParameter GetParameter(this CosViewModel model)
    {
        return new SinCosParameter
        {
            Frequency = model.Frequency,
            FrequencyMeasureId = model.SelectedFrequencyMeasure.Id,
            Phase = model.Phase,
            PhaseMeasureId = model.SelectedPhaseMeasure.Id,
            Amplitude = model.Amplitude,
            Duration = model.Duration
        };
    }

    public static SincParameter GetParameter(this SincViewModel model)
    {
        return new SincParameter
        {
            Frequency = model.Frequency,
            FrequencyMeasureId = model.SelectedFrequencyMeasure.Id,
            xMax = model.xMax,
            xMin = model.xMin
        };
    }
}
