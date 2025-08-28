using ruby_plotter.app.Contracts.Commands;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using System.Windows.Controls;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// y = A * sin(x) + B
/// </summary>
public class SinViewModel : ValidationViewModelBase
{
    private double _amplitude;
    private double _phase;
    private double _frequency;
    private double _duration;
    private readonly SinDefaultSettings _defaultSettings;

    public SinViewModel(SinCosParameter parameter, SinDefaultSettings sinDefaultSettings)
    {
        _amplitude = parameter.Amplitude;
        _phase = parameter.Phase;
        _frequency = parameter.Frequency;
        _frequencyMeasureId = FrequencyMeasures.Any(p => p.Id == parameter.FrequencyMeasureId) ?
            parameter.FrequencyMeasureId : FrequencyMeasures.First().Id;
        _phaseMeasureId = PhaseMeasures.Any(p => p.Id == parameter.PhaseMeasureId) ?
            parameter.PhaseMeasureId : PhaseMeasures.First().Id;
        _duration = parameter.Duration;
        _defaultSettings = sinDefaultSettings;

        RefreshCommand = new DelegateCommand(Refresh);
    }

    /// <summary>
    /// This command is used to refresh the values of the Sine wave parameters.
    /// </summary>
    public DelegateCommand RefreshCommand { get; }

    /// <summary>
    /// Gets or sets the Amplitude of the Sine wave.
    /// </summary>
    public double Amplitude
    {
        get => _amplitude;
        set
        {
            if (Math.Abs(_amplitude - value) > 0.00001)
            {
                _amplitude = value;
                OnPropertyChanged(nameof(Amplitude));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Phase of the Sine wave. 
    /// </summary>
    /// <remarks>
    ///     The value of the Phase is in degrees.
    /// </remarks>
    public double Phase
    {
        get
        {
            return _phase * PhaseMeasures.First(p => p.Id == _phaseMeasureId).Koeff;
        }
        set
        {
            double _valueDegree = value / PhaseMeasures.First(p => p.Id == _phaseMeasureId).Koeff;

            if (_phase != _valueDegree)
            {
                _phase = _valueDegree;
                OnPropertyChanged(nameof(Phase));
            }
        }
    }

    public override MeasureItem SelectedPhaseMeasure
    {
        get
        {
            return base.SelectedPhaseMeasure;
        }
        set
        {
            if (value.Id != _phaseMeasureId)
            {
                base.SelectedPhaseMeasure = value;
                OnPropertyChanged(nameof(SelectedPhaseMeasure));
                OnPropertyChanged(nameof(Phase));
            }
        }
    }
    public double PhaseDegrees => _phase;

    /// <summary>
    /// Gets or sets the Frequency of the Sine wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Frequency is in Hz.
    /// </remarks>
    public double Frequency
    {
        get => _frequency;
        set
        {
            if (Math.Abs(_frequency - value) > 0.00001)
            {
                _frequency = value;
                OnPropertyChanged(nameof(Frequency));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Duration of the Sine wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Duration is in seconds.
    /// </remarks>
    public double Duration
    {
        get => _duration;
        set
        {
            if (Math.Abs(_duration - value) > 0.00001)
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    /// <summary>
    /// This method is used to refresh the values of the Sine wave parameters.
    /// </summary>
    /// <param name="parameters">
    ///     The parameters that are used to refresh the values of the Sine wave parameters.
    /// </param>
    private void Refresh(object? parameters)
    {
        var textBox = parameters as TextBox;

        switch (textBox?.Name)
        {
            case "FrequencyTextBox":
                if (double.TryParse(textBox.Text, out var frequency))
                {
                    Frequency = frequency;
                }
                break;

            case "AmplitudeTextBox":
                if (double.TryParse(textBox.Text, out var amplitude))
                {
                    Amplitude = amplitude;
                }
                break;

            case "PhaseTextBox":
                if (int.TryParse(textBox.Text, out var phase))
                {
                    Phase = phase;
                }
                break;

            case "DurationTextBox":
                if (double.TryParse(textBox.Text, out var duration))
                {
                    Duration = duration;
                }
                break;

            default:
                break;
        }
    }
}
