using ruby_plotter.app.Contracts.Commands;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using System.Windows.Controls;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// This class represents the ViewModel for the Cosine wave parameters.
/// </summary>
public class CosViewModel : ValidationViewModelBase
{
    private double _amplitude;
    private double _phase;
    private double _frequncy;
    private double _duration;
    private readonly CosDefaultSettings _defaultSettings;

    public CosViewModel(SinCosParameter parameter, CosDefaultSettings cosDefaultSettings)
    {
        _amplitude = parameter.Amplitude;
        _phase = parameter.Phase;
        _frequncy = parameter.Frequency;
        _frequencyMeasureId = FrequencyMeasures.Any(p => p.Id == parameter.FrequencyMeasureId) ?
            parameter.FrequencyMeasureId : FrequencyMeasures.First().Id;
        _phaseMeasureId = PhaseMeasures.Any(p => p.Id == parameter.PhaseMeasureId) ?
            parameter.PhaseMeasureId : PhaseMeasures.First().Id;
        _duration = parameter.Duration;
        _defaultSettings = cosDefaultSettings;

        RefreshCommand = new DelegateCommand(Refresh);
    }

    /// <summary>
    /// This command is used to refresh the values of the Cos wave parameters.
    /// </summary>
    public DelegateCommand RefreshCommand { get; }

    /// <summary>
    /// Gets or sets the Amplitude of the Cos wave.
    /// </summary>
    public double Amplitude
    {
        get => _amplitude;
        set
        {
            if (Math.Abs(_amplitude - value) > 0.00001)
            {
                // Validate amplitude
                ClearErrors();

                if (value > _defaultSettings.AmplitudeMax)
                {
                    AddError($"Value can't be more than {_defaultSettings.AmplitudeMax}");
                }
                else if (value < _defaultSettings.AmplitudeMin)
                {
                    AddError($"Value can't be less than {_defaultSettings.AmplitudeMin}");
                }

                _amplitude = value;
                OnPropertyChanged(nameof(Amplitude));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Phase of the Cos wave. 
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
    /// Gets or sets the Frequency of the Cos wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Frequency is in Hz.
    /// </remarks>
    public double Frequency
    {
        get => _frequncy;
        set
        {
            if (Math.Abs(_frequncy - value) > 0.00001 || HasErrors)
            {
                _frequncy = value;
                OnPropertyChanged(nameof(Frequency));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Duration of the Cos wave.
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
                //ClearErrors();

                //if (value <= _defaultSettings.DurationMin)
                //{
                //    AddError($"Value can't be less or equals {_defaultSettings.DurationMin}");
                //}
                //else if (value > _defaultSettings.DurationMax)
                //{
                //    AddError($"Value can't be more than {_defaultSettings.DurationMax} sec");
                //}

                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    /// <summary>
    /// This method is used to refresh the values of the Cos wave parameters.
    /// </summary>
    /// <param name="parameters">
    ///     The parameters that are used to refresh the values of the Cos wave parameters.
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
