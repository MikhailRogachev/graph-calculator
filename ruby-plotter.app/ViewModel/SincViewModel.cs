using ruby_plotter.app.Contracts.Commands;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;
using System.Windows.Controls;

namespace ruby_plotter.app.ViewModel;


/// <summary>
/// https://en.wikipedia.org/wiki/Sinc_function
/// </summary>
public class SincViewModel : ValidationViewModelBase
{
    private double _frequncy;
    private double _xmin;
    private double _xmax;
    private readonly SincDefaultSettings _sincDefaultSettings;

    public DelegateCommand RefreshCommand { get; }

    public SincViewModel(SincParameter parameter, SincDefaultSettings sincDefaultSettings)
    {
        _frequncy = parameter.Frequency;
        _xmin = parameter.xMin;
        _xmax = parameter.xMax;
        _sincDefaultSettings = sincDefaultSettings;

        RefreshCommand = new DelegateCommand(Refresh);
    }

    /// <summary>
    /// Gets or sets the Frequency of the Sine wave.
    /// </summary>
    /// <remarks>
    ///     The value of the Phase is in kHz.
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

                if (value <= _sincDefaultSettings.FrequencyMin)
                {
                    AddError($"Value can't be less or equals {_sincDefaultSettings.FrequencyMin}");
                }
                else if (value > _sincDefaultSettings.FrequencyMax)
                {
                    AddError($"Value can't be more then {_sincDefaultSettings.FrequencyMax}kHz");
                }

                _frequncy = value;
                OnPropertyChanged(nameof(Frequency));
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
            ClearErrors();

            if (Math.Abs(_xmin - value) > 0.001)
            {
                if (value < _sincDefaultSettings.Xmin)
                {
                    AddError($"Value can't be less than {_sincDefaultSettings.Xmin}");
                }
                else if (value >= xMax)
                {
                    AddError("xMin can't be euals or greater than xMax");
                }

                _xmin = value;
                OnPropertyChanged(nameof(xMin));
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
                ClearErrors();

                if (value > _sincDefaultSettings.Xmax)
                {
                    AddError($"Value can't be greater then {_sincDefaultSettings.Xmax}");
                }
                else if (value <= xMin)
                {
                    AddError("Value can't be less than xMin");
                }

                _xmax = value;
                OnPropertyChanged(nameof(xMax));
            }
        }
    }

    /// <summary>
    /// This method is used to refresh the values of the Sinc wave parameters.
    /// </summary>
    /// <param name="parameters">
    ///     The parameters that are used to refresh the values of the Sinc wave parameters.
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

            case "xMinTextBox":
                if (double.TryParse(textBox.Text, out var xmin))
                {
                    xMin = xmin;
                }
                break;

            case "xMaxTextBox":
                if (int.TryParse(textBox.Text, out var xmax))
                {
                    xMax = xmax;
                }
                break;

            default:
                break;
        }
    }
}
