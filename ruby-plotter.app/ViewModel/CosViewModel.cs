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
    private int _phase;
    private double _frequncy;
    private double _duration;
    private readonly CosDefaultSettings _defaultSettings;

    public CosViewModel(SinCosParameter parameter, CosDefaultSettings cosDefaultSettings)
    {
        _amplitude = cosDefaultSettings.AmplitudeDefault;
        _phase = parameter.Phase;
        _frequncy = cosDefaultSettings.FrequencyDefault;
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
                    _amplitude = _defaultSettings.AmplitudeMax;
                }
                else if (value < _defaultSettings.AmplitudeMin)
                {
                    AddError($"Value can't be less than {_defaultSettings.AmplitudeMin}");
                    _amplitude = _defaultSettings.AmplitudeMin;
                }
                else
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
    public int Phase
    {
        get => _phase;
        set
        {
            if (_phase != value)
            {
                ClearErrors();

                if (value > _defaultSettings.PhaseMax)
                {
                    AddError($"Value can't be more than {_defaultSettings.PhaseMax}");
                    _phase = _defaultSettings.PhaseMax;
                }
                else if (value < _defaultSettings.PhaseMin)
                {
                    AddError($"Value can't be less than {_defaultSettings.PhaseMin}");
                    _phase = _defaultSettings.PhaseMin;
                }
                else
                {
                    _phase = value;
                }

                OnPropertyChanged(nameof(Phase));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Frequency of the Cos wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Frequency is in kHz.
    /// </remarks>
    public double Frequency
    {
        get => _frequncy;
        set
        {
            if (Math.Abs(_frequncy - value) > 0.00001)
            {
                // Validate frequency
                ClearErrors();

                if (value <= 0)
                {
                    AddError($"Value can't be less or equals 0kHz");
                    _frequncy = _defaultSettings.FrequencyDefault;
                }
                else if (value > _defaultSettings.FrequencyMax)
                {
                    AddError($"Value can't be more then {_defaultSettings.FrequencyMax}kHz");
                    _frequncy = _defaultSettings.FrequencyMax;
                }
                else
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
                ClearErrors();

                if (value <= _defaultSettings.DurationMin)
                {
                    AddError($"Value can't be less or equals {_defaultSettings.DurationMin}");
                    _duration = _defaultSettings.DurationMin;
                }
                else if (value > _defaultSettings.DurationMax)
                {
                    AddError($"Value can't be more than {_defaultSettings.DurationMax} sec");
                    _duration = _defaultSettings.DurationMax;
                }
                else
                {
                    _duration = value;
                }

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
