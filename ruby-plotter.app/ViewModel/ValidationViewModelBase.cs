using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ruby_plotter.app.ViewModel;

public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorByPropertyName = new();
    public bool HasErrors => _errorByPropertyName.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        return !string.IsNullOrWhiteSpace(propertyName) && _errorByPropertyName.ContainsKey(propertyName)
            ? _errorByPropertyName[propertyName]
            : Enumerable.Empty<string>();
    }

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs arg)
    {
        ErrorsChanged?.Invoke(this, arg);
    }

    protected virtual void AddError(string errorMessage,
        [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return;
        }

        if (!_errorByPropertyName.ContainsKey(propertyName))
        {
            _errorByPropertyName[propertyName] = new List<string>();
        }

        if (!_errorByPropertyName[propertyName].Contains(errorMessage))
        {
            _errorByPropertyName[propertyName].Add(errorMessage);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return;
        }

        if (_errorByPropertyName.ContainsKey(propertyName))
        {
            _errorByPropertyName.Remove(propertyName);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }
}
