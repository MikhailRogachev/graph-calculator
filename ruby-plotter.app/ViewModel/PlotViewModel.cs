using ruby_plotter.app.Contracts.Extensions;
using ruby_plotter.app.Contracts.Options;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WPF;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// this class is the ViewModel for the PlotView.
/// </summary>
public class PlotViewModel : ViewModelBase
{
    private readonly PloterDefaultSettings _settings;
    public WpfPlot Graphs { get; } = new WpfPlot();

    private Dictionary<string, Scatter> _plotScatters = new Dictionary<string, Scatter>();

    public PlotViewModel(PloterDefaultSettings settings)
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
    /// <param name="sincView">SincViewModel</param>
    public void Plot(SinViewModel? sinView = null, CosViewModel? cosView = null, SincViewModel? sincView = null)
    {
        Graphs.Plot.Clear();

        SinViewInit(sinView);
        CosViewInit(cosView);
        SincViewInit(sincView);
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
    /// <param name="scatterType">SinViewModel, CosViewModel, SincViewModel</param>
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

        //get frequency measure koeff
        double koeff = FrequencyMeasures.First(p => p.Id == model.SelectedFrequencyMeasure.Id).Koeff;

        var source = FuncGenerator.Sin(
            frequency: model.Frequency * koeff,
            duration: model.Duration,
            amplitude: model.Amplitude,
            phase: model.Phase,
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

        var source = FuncGenerator.Sin(
            frequency: model.Frequency,
            duration: model.Duration,
            amplitude: model.Amplitude,
            phase: model.Phase + 90,
            framerate: _settings.Framerate
            );

        var scatter = GrapfPlotting(source.Ts, source.Ys, "Cosine", Colors.Green);
        AddPlotScatter(typeof(CosViewModel), scatter);
    }

    private void SincViewInit(SincViewModel? model)
    {
        RemovePlotScatter(typeof(SincViewModel));
        if (model == null)
        {
            return;
        }

        model.PropertyChanged += (sender, args) =>
        {
            PlotSinc(model);
        };
        PlotSinc(model);
    }

    private void PlotSinc(SincViewModel model)
    {
        RemovePlotScatter(typeof(SincViewModel));

        if (model.HasErrors)
        {
            return;
        }

        var source = FuncGenerator.SinCardinal(
            frequency: model.Frequency,
            xEnd: model.xMax,
            xStart: model.xMin,
            framerate: _settings.Framerate
            );
        var scatter = GrapfPlotting(source.Ts, source.Ys, "sinc", Colors.Black);
        AddPlotScatter(typeof(SincViewModel), scatter);
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
