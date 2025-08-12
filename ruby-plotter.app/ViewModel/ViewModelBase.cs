using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ruby_plotter.app.ViewModel;

/// <summary>
/// This is the base class for all view models in the Ruby Plotter application.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
