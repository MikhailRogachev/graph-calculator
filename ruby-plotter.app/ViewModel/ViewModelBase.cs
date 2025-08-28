using ruby_plotter.app.Contracts.Parameters;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// This is the base class for all view models in the Ruby Plotter application.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    protected int _frequencyMeasureId = 1;
    protected int _phaseMeasureId = 1;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual ObservableCollection<MeasureItem> FrequencyMeasures { get; set; }
        = new ObservableCollection<MeasureItem>(new List<MeasureItem>()
        {
            new MeasureItem(){Id = 1, Name = "Hz", Koeff = 1 },
            new MeasureItem(){Id = 2, Name = "kHz", Koeff = 1000 }
        });

    public virtual MeasureItem SelectedFrequencyMeasure
    {
        get
        {
            return FrequencyMeasures.First(p => p.Id == _frequencyMeasureId);
        }
        set
        {
            if (value.Id != _frequencyMeasureId)
            {
                _frequencyMeasureId = value.Id;
                OnPropertyChanged(nameof(SelectedFrequencyMeasure));
            }

        }
    }

    public virtual ObservableCollection<MeasureItem> PhaseMeasures { get; set; }
        = new ObservableCollection<MeasureItem>(new List<MeasureItem>()
        {
            new MeasureItem(){Id = 1, Name = "Degrees", Koeff =  1},
            new MeasureItem(){Id = 2, Name = "Radians", Koeff = Math.PI / 180 }
        });

    public virtual MeasureItem SelectedPhaseMeasure
    {
        get => PhaseMeasures.First(p => p.Id == _phaseMeasureId);
        set => _phaseMeasureId = value.Id;
    }
}
