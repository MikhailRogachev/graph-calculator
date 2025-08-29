using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;


/// <summary>
/// https://en.wikipedia.org/wiki/Sinc_function
/// </summary>
public class SincViewModel : ValidationViewModelBase
{
    private double _frequency;
    private double _xmax;
    private double _xmin;

    public SincViewModel(SincParameter parameter)
    {
        _frequency = parameter.Frequency;
        _frequencyMeasureId = FrequencyMeasures.Any(p => p.Id == parameter.FrequencyMeasureId) ?
            parameter.FrequencyMeasureId : FrequencyMeasures.First().Id;
        _xmin = parameter.xMin;
        _xmax = parameter.xMax;
    }

    /// <summary>
    /// Gets or sets the Frequency of the Sine wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Phase is in Hz.
    /// </remarks>
    public double Frequency
    {
        get => _frequency * FrequencyMeasures.First(p => p.Id == _frequencyMeasureId).Koeff;
        set
        {
            double _valueHz = value / FrequencyMeasures.First(p => p.Id == _frequencyMeasureId).Koeff;

            if (Math.Abs(_frequency - _valueHz) > 0.00001)
            {
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
    /// Gets or sets the value for the Sinc function end point.
    /// </summary>
    /// <remarks>
    ///     The value of the xMax is in seconds.
    /// </remarks>
    public double xMax
    {
        get => _xmax;
        set
        {
            if (Math.Abs(_xmax - value) > 0.001)
            {
                _xmax = value;
                OnPropertyChanged(nameof(xMax));
            }
        }
    }

    /// <summary>
    /// Gets or sets the value for the Sinc function start point.
    /// </summary>
    /// <remarks>
    ///     The value of the xMin is in seconds.
    /// </remarks>
    public double xMin
    {
        get => _xmin;
        set
        {
            if (Math.Abs(_xmin - value) > 0.001)
            {
                _xmin = value;
                OnPropertyChanged(nameof(xMin));
            }
        }
    }
}
