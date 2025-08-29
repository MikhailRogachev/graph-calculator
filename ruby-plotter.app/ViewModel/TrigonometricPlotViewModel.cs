using ruby_plotter.app.Contracts.Extensions;
using ruby_plotter.app.Contracts.Options;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WPF;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// this class is the ViewModel for the PlotView.
/// </summary>
public class TrigonometricPlotViewModel : ViewModelBase
{
    private readonly PloterDefaultSettings _settings;
    public WpfPlot Graphs { get; } = new WpfPlot();

    private Dictionary<string, Scatter> _plotScatters = new Dictionary<string, Scatter>();

    public TrigonometricPlotViewModel(PloterDefaultSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        AxisInitialization();
    }

    #region procedures and functions

    /// <summary>
    /// This procedure plots the data on the graph.
    /// </summary>
    /// <param name="sinView">SinViewModel</param>
    /// <param name="cosView">CosViewModel</param>
    public void Plot(SinViewModel? sinView = null, CosViewModel? cosView = null)
    {
        Graphs.Plot.Clear();
        SinViewInit(sinView);
        CosViewInit(cosView);
        Graphs.Refresh();
    }


    /// <summary>
    /// This procedure removes the plot scatter from the grapf in case 
    /// the function VuewModel is null.
    /// </summary>
    /// <param name="scatterType"></param>
    private void RemovePlotScatter(Type scatterType)
    {
        var key = scatterType.Name;
        if (_plotScatters.ContainsKey(key))
        {
            Graphs.Plot.Remove(_plotScatters[key]);
            _plotScatters.Remove(key);
            Graphs.Refresh();
        }
    }

    /// <summary>
    /// This procedure adds a plot scatter to the graph.
    /// </summary>
    /// <param name="scatterType">SinViewModel, CosViewModel</param>
    /// <param name="scatter">Scatter</param>
    private void AddPlotScatter(Type scatterType, Scatter scatter)
    {
        var key = scatterType.Name;
        if (!_plotScatters.ContainsKey(key))
        {
            _plotScatters.Add(key, scatter);
        }
    }

    private void SinViewInit(SinViewModel? model)
    {
        RemovePlotScatter(typeof(SinViewModel));

        if (model == null)
        {
            return;
        }

        model.PropertyChanged += (sender, args) =>
        {
            PlotSin(model);
        };

        PlotSin(model);
    }

    private void PlotSin(SinViewModel model)
    {
        RemovePlotScatter(typeof(SinViewModel));

        if (model == null)
        {
            return;
        }

        var source = FuncGenerator.Sin(
            frequency: model.FrequencyHz,
            duration: model.Duration,
            amplitude: model.Amplitude,
            phase: model.PhaseDegrees,
            framerate: _settings.Framerate
            );

        var scatter = GrapfPlotting(source.Ts, source.Ys, "Sine", Colors.Salmon);
        AddPlotScatter(typeof(SinViewModel), scatter);
    }

    private void CosViewInit(CosViewModel? model)
    {
        RemovePlotScatter(typeof(CosViewModel));

        if (model == null)
        {
            return;
        }

        model.PropertyChanged += (sender, args) =>
        {
            PlotCos(model);
        };

        PlotCos(model);
    }

    private void PlotCos(CosViewModel model)
    {
        RemovePlotScatter(typeof(CosViewModel));

        if (model.HasErrors)
        {
            return;
        }

        var source = FuncGenerator.Cos(
            frequency: model.FrequencyHz,
            duration: model.Duration,
            amplitude: model.Amplitude,
            phase: model.PhaseDegrees,
            framerate: _settings.Framerate
            );

        var scatter = GrapfPlotting(source.Ts, source.Ys, "Cosine", Colors.Green);
        AddPlotScatter(typeof(CosViewModel), scatter);
    }


    /// <summary>
    /// This procedure initializes the axis of the graph.
    /// </summary>
    private void AxisInitialization()
    {
        // axis limits
        Graphs.Plot.Axes.SetLimits(_settings.Xmin, _settings.Xmax, _settings.Ymin, _settings.Ymax);
        // axis labels
        Graphs.Plot.XLabel(_settings.Xlabel);
        Graphs.Plot.YLabel(_settings.Ylabel);
        // legend
        Graphs.Plot.Legend.FontSize = 32;
        Graphs.Plot.Legend.ShadowColor = Colors.Blue.WithOpacity(.2);
        Graphs.Plot.Legend.ShadowOffset = new(10, 10);
        Graphs.Refresh();
    }

    /// <summary>
    /// This procedure plots the data on the graph.
    /// </summary>
    /// <param name="ts">Time marks array</param>
    /// <param name="ys">Amplitude marks array</param>
    /// <param name="graphName">The graph's name</param>
    /// <param name="color">The graph's color</param>
    /// <returns>Scatter builded</returns>
    private Scatter GrapfPlotting(double[] ts, double[] ys, string graphName, Color color)
    {
        var scatter = Graphs.Plot.Add.Scatter(ts, ys, color);

        scatter.LegendText = graphName;
        scatter.MarkerSize = 0;
        scatter.LineWidth = _settings.LineWidth;
        Graphs.Refresh();

        return scatter;
    }

    #endregion
}
