using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;

public class TrigonometricViewModel : ValidationViewModelBase
{
    private double _amplitude;
    private double _duration;
    private double _frequency;
    private double _phase;
    private readonly TrigonometricValidationSettings _validationSettings;

    public TrigonometricViewModel(SinCosParameter parameter, TrigonometricValidationSettings validationSettings)
    {
        ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));
        ArgumentNullException.ThrowIfNull(validationSettings, nameof(validationSettings));

        _amplitude = parameter.Amplitude;
        _phase = parameter.Phase;
        _frequency = parameter.Frequency;
        _frequencyMeasureId = FrequencyMeasures.Any(p => p.Id == parameter.FrequencyMeasureId) ?
            parameter.FrequencyMeasureId : FrequencyMeasures.First().Id;
        _phaseMeasureId = PhaseMeasures.Any(p => p.Id == parameter.PhaseMeasureId) ?
            parameter.PhaseMeasureId : PhaseMeasures.First().Id;
        _duration = parameter.Duration;

        _validationSettings = validationSettings;
    }

    /// <summary>
    /// Gets or sets the Amplitude of the Trigonometric function.
    /// </summary>
    public virtual double Amplitude
    {
        get => _amplitude;
        set
        {
            ClearErrors(nameof(Amplitude));

            if (Math.Abs(_amplitude - value) > 0.00001)
            {
                if (value < _validationSettings.AmplitudeMin || value > _validationSettings.AmplitudeMax)
                {
                    AddError($"The Amplitude must be between {_validationSettings.AmplitudeMin} and {_validationSettings.AmplitudeMax}.", nameof(Amplitude));
                }

                _amplitude = value;
                OnPropertyChanged(nameof(Amplitude));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Duration of the Cos wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Duration is in seconds.
    /// </remarks>
    public virtual double Duration
    {
        get => _duration;
        set
        {
            ClearErrors(nameof(Duration));

            if (Math.Abs(_duration - value) > 0.00001)
            {
                if (value <= 0 || value > _validationSettings.DurationMax)
                {
                    AddError($"The Duration must be between 0 and {_validationSettings.DurationMax}.", nameof(Duration));
                }

                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    /// <summary>
    /// Gets or sets the Frequency of the Cos wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Frequency is in Hz.
    /// </remarks>
    public virtual double Frequency
    {
        get => _frequency * FrequencyMeasures.First(p => p.Id == _frequencyMeasureId).Koeff;
        set
        {
            ClearErrors(nameof(Frequency));
            double _valueHz = value / FrequencyMeasures.First(p => p.Id == _frequencyMeasureId).Koeff;

            if (Math.Abs(_frequency - _valueHz) > 0.00001)
            {
                if (_valueHz <= _validationSettings.FrequencyHzMin || _valueHz > _validationSettings.FrequencyHzMax)
                {
                    AddError($"The Frequency must be between {_validationSettings.FrequencyHzMin} and {_validationSettings.FrequencyHzMax} Hz.", nameof(Frequency));
                }

                _frequency = _valueHz;
                OnPropertyChanged(nameof(Frequency));
            }
        }
    }

    /// <summary>
    /// Get the current Frequency value in Hz.
    /// </summary>
    /// <remarks>
    ///     This value is using for the function calculation only.
    /// </remarks>
    public virtual double FrequencyHz => _frequency;

    /// <summary>
    /// Gets or sets the Phase of the Cos wave. 
    /// </summary>
    /// <remarks>
    ///     The value of the Phase is in degrees.
    /// </remarks>
    public virtual double Phase
    {
        get
        {
            return _phase * PhaseMeasures.First(p => p.Id == _phaseMeasureId).Koeff;
        }
        set
        {
            ClearErrors(nameof(Phase));
            double _valueDegree = value / PhaseMeasures.First(p => p.Id == _phaseMeasureId).Koeff;

            if (_phase != _valueDegree)
            {
                if (_valueDegree < _validationSettings.PhaseMin || _valueDegree > _validationSettings.PhaseMax)
                {
                    AddError($"The Phase must be between {_validationSettings.PhaseMin} and {_validationSettings.PhaseMax} degrees.", nameof(Phase));
                }
                _phase = _valueDegree;
                OnPropertyChanged(nameof(Phase));
            }
        }
    }

    /// <summary>
    /// Get the current phase value in Degree.
    /// </summary>
    /// <remarks>
    ///     This value is using for the function calculation only.
    /// </remarks>
    public virtual double PhaseDegrees => _phase;

    /// <summary>
    /// Get or set the current frequency measure.
    /// </summary>
    public override MeasureItem SelectedFrequencyMeasure
    {
        get
        {
            return base.SelectedFrequencyMeasure;
        }
        set
        {
            if (value.Id != _frequencyMeasureId)
            {
                base.SelectedFrequencyMeasure = value;
                OnPropertyChanged(nameof(SelectedFrequencyMeasure));
                OnPropertyChanged(nameof(Frequency));
            }

        }
    }

    /// <summary>
    /// Get or set Selected Phase measure item.
    /// </summary>
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
}
