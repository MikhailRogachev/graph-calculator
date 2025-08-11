using System.Windows.Input;

namespace ruby_plotter.app.Contracts.Commands;

/// <summary>
/// This command implements the ICommand interface, enabling you 
/// to define the commanding behavior for UI elements.
/// </summary>
public class DelegateCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);
    public void Execute(object? parameter) => _execute(parameter);
}
