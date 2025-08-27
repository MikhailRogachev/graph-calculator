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

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual ObservableCollection<FrequencyMeasure> FrequencyMeasures { get; set; }
        = new ObservableCollection<FrequencyMeasure>(new List<FrequencyMeasure>()
        {
            new FrequencyMeasure(){Id = 1, Name = "Hz", Koeff = 1 },
            new FrequencyMeasure(){Id = 2, Name = "kHz", Koeff = 1000 }
        });

    public virtual FrequencyMeasure SelectedFrequencyMeasure
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
}
