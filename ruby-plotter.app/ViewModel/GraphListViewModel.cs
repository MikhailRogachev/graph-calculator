using Microsoft.Extensions.Options;
using ruby_plotter.app.Contracts.Commands;
using ruby_plotter.app.Contracts.Enums;
using ruby_plotter.app.Contracts.Interfaces;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Contracts.Parameters;

namespace ruby_plotter.app.ViewModel;

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
    public DelegateCommand SinViewCommand { get; }
    public DelegateCommand CosViewCommand { get; }
    public DelegateCommand SincViewCommand { get; }

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
    public VisibleMode SinModelVisibleMode => SinViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;
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
    public VisibleMode CosModelVisibleMode => CosViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;
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
    public VisibleMode SincModelVisibleMode => SincViewModel == null ? VisibleMode.Invisible : VisibleMode.Visible;

    #endregion

    #region private methods

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
                    @set = new SinCosParameter
                    {
                        Frequency = 0.05,
                        Phase = 0,
                        Amplitude = 1,
                        Duration = 2.0
                    };
                }

                SinViewModel = new SinViewModel(@set, _appSettings.SinDefaultSettings);
            }
        }
        else
        {
            if (SinViewModel != null)
            {
                var @set = new SinCosParameter
                {
                    Frequency = SinViewModel.Frequency,
                    Phase = SinViewModel.Phase,
                    Amplitude = SinViewModel.Amplitude,
                    Duration = SinViewModel.Duration
                };

                _serializerService.SerializeToFile(@set, _sinParameterFileName);
                SinViewModel = default(SinViewModel);
            }

        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

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
                    @set = new SinCosParameter
                    {
                        Frequency = 0.05,
                        Phase = 0,
                        Amplitude = 1,
                        Duration = 2.0
                    };
                }

                CosViewModel = new CosViewModel(@set, _appSettings.CosDefaultSettings);
            }
        }
        else
        {
            if (CosViewModel != null)
            {
                var @set = new SinCosParameter
                {
                    Frequency = CosViewModel.Frequency,
                    Phase = CosViewModel.Phase,
                    Amplitude = CosViewModel.Amplitude,
                    Duration = CosViewModel.Duration
                };

                _serializerService.SerializeToFile(@set, _cosParameterFileName);
                CosViewModel = default(CosViewModel);
            }
        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

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
                    @set = new SincParameter
                    {
                        Frequency = 0.05,
                        xMax = 2.0,
                        xMin = -2.0
                    };
                }

                SincViewModel = new SincViewModel(@set, _appSettings.SincDefaultSettings);
            }
        }
        else
        {
            if (SincViewModel != null)
            {
                var @set = new SincParameter
                {
                    Frequency = SincViewModel.Frequency,
                    xMax = SincViewModel.xMax,
                    xMin = SincViewModel.xMin
                };

                _serializerService.SerializeToFile(@set, _sincParameterFileName);
                SincViewModel = default(SincViewModel);
            }

        }

        PlotView.Plot(SinViewModel, CosViewModel, SincViewModel);
    }

    #endregion
}
