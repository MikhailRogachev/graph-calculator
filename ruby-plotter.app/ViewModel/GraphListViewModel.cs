using Microsoft.Extensions.Options;
using ruby_plotter.app.Contracts.Commands;
using ruby_plotter.app.Contracts.Enums;
using ruby_plotter.app.Contracts.Extensions;
using ruby_plotter.app.Contracts.Interfaces;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// This class represents the view model for the graph list.
/// </summary>
public class GraphListViewModel : ViewModelBase
{
    private SinViewModel? _sinViewModel;
    private CosViewModel? _cosViewModel;
    private SincViewModel? _sincViewModel;
    private PlotViewModel? _plotView;

    private readonly AppDefaultSettings _appSettings;
    private readonly ISerializerService _serializerService;
    private readonly string _sinParameterFileName = "sinviewparameter.json";
    private readonly string _cosParameterFileName = "cosviewparameter.json";
    private readonly string _sincParameterFileName = "sincviewparameter.json";

    public GraphListViewModel(
        IOptions<AppDefaultSettings> options,
        ISerializerService serializerService)
    {
        _appSettings = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
        _plotView = new PlotViewModel(_appSettings.PloterDefaultSettings);
        _serializerService = serializerService ?? throw new ArgumentNullException(nameof(serializerService));

        SinViewCommand = new DelegateCommand(SinViewUpdate);
        CosViewCommand = new DelegateCommand(CosViewUpdate);
        SincViewCommand = new DelegateCommand(SincViewUpdate);
    }

    #region properties

    public PlotViewModel PlotView => _plotView!;

    /// <summary>
    /// Get the command to create or update the Sin view model.
    /// </summary>
    public DelegateCommand SinViewCommand { get; }

    /// <summary>
    /// Get the command to create or update the Cos view model.
    /// </summary>
    public DelegateCommand CosViewCommand { get; }

    /// <summary>
    /// Get the command to create or update the Sinc view model.
    /// </summary>
    public DelegateCommand SincViewCommand { get; }

    /// <summary>
    /// Get or set the Sin view model.
    /// </summary>
    public SinViewModel? SinViewModel
    {
        get => _sinViewModel;
        set
        {
            if (_sinViewModel != value)
            {
                _sinViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SinModelVisibleMode));
            }
        }
    }

    /// <summary>
    /// Get the visibility mode of the Sin model.
    /// </summary>
    public VisibleMode SinModelVisibleMode => SinViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;

    /// <summary>
    /// Get or set the Cos view model.
    /// </summary>
    public CosViewModel? CosViewModel
    {
        get => _cosViewModel;
        set
        {
            if (_cosViewModel != value)
            {
                _cosViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CosModelVisibleMode));
            }
        }
    }

    /// <summary>
    /// Get the visibility mode of the Cos model.
    /// </summary>
    public VisibleMode CosModelVisibleMode => CosViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;

    /// <summary>
    /// Get or set the Sinc view model.
    /// </summary>
    public SincViewModel? SincViewModel
    {
        get => _sincViewModel;
        set
        {
            if (_sincViewModel != value)
            {
                _sincViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SincModelVisibleMode));
            }
        }
    }

    /// <summary>
    /// Get the visibility mode of the Sinc model.
    /// </summary>
    public VisibleMode SincModelVisibleMode => SincViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;

    #endregion

    #region private methods

    /// <summary>
    /// This method updates the Sin view model based on the provided parameter.
    /// The parameters for the Sin view model are loaded from a last result file. If this file
    /// doesn't exist, default parameters are used.
    /// In case the view model is not checked, the current parameters are saved to a file and 
    /// the current view model is set to null.
    /// </summary>
    /// <param name="parameter">typeof(SinCosParameter)</param>
    private void SinViewUpdate(object? parameter)
    {
        var ischecked = (bool)parameter!;

        if (ischecked)
        {
            if (SinViewModel == null)
            {
                var @set = _serializerService.DeserializeFromFile<SinCosParameter>(_sinParameterFileName);
                if (@set == null)
                {
                    @set = ParameterExtensions.GetDefaultParameter();
                }

                SinViewModel = new SinViewModel(@set);
            }
        }
        else
        {
            if (SinViewModel != null)
            {
                var @set = SinViewModel.GetParameter();

                _serializerService.SerializeToFile(@set, _sinParameterFileName);
                SinViewModel = default(SinViewModel);
            }

        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

    /// <summary>
    /// This method updates the Cos view model based on the provided parameter.
    /// The parameters for the Cos view model are loaded from a last result file. If this file
    /// doesn't exist, default parameters are used.
    /// In case the view model is not checked, the current parameters are saved to a file and 
    /// the current view model is set to null.
    /// </summary>
    /// <param name="parameter">typeof(SinCosParameter)</param>
    private void CosViewUpdate(object? parameter)
    {
        var ischecked = (bool)parameter!;

        if (ischecked)
        {
            if (CosViewModel == null)
            {
                var @set = _serializerService.DeserializeFromFile<SinCosParameter>(_cosParameterFileName);
                if (@set == null)
                {
                    @set = ParameterExtensions.GetDefaultParameter();
                }

                CosViewModel = new CosViewModel(@set, _appSettings.CosDefaultSettings);
            }
        }
        else
        {
            if (CosViewModel != null)
            {
                var @set = CosViewModel.GetParameter();

                _serializerService.SerializeToFile(@set, _cosParameterFileName);
                CosViewModel = default(CosViewModel);
            }
        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

    /// <summary>
    /// This method updates the Sinc view model based on the provided parameter.
    /// The parameters for the Sinc view model are loaded from a last result file. If this file
    /// doesn't exist, default parameters are used.
    /// In case the view model is not checked, the current parameters are saved to a file and 
    /// the current view model is set to null.
    /// </summary>
    /// <param name="parameter">typeof(SincParameter)</param>
    private void SincViewUpdate(object? parameter)
    {
        var ischecked = (bool)parameter!;

        if (ischecked)
        {
            if (SincViewModel == null)
            {
                var @set = _serializerService.DeserializeFromFile<SincParameter>(_sincParameterFileName);
                if (@set == null)
                {
                    @set = ParameterExtensions.GetDefaultSincParameter();
                }

                SincViewModel = new SincViewModel(@set, _appSettings.SincDefaultSettings);
            }
        }
        else
        {
            if (SincViewModel != null)
            {
                var @set = SincViewModel.GetParameter();

                _serializerService.SerializeToFile(@set, _sincParameterFileName);
                SincViewModel = default(SincViewModel);
            }

        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

    #endregion
}
