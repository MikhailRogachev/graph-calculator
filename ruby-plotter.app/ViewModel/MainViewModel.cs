namespace ruby_plotter.app.ViewModel;

/// <summary>
/// The main view model for the Ruby Plotter application.
/// </summary>
public class MainViewModel : ViewModelBase
{
    public MainViewModel(GraphListViewModel graphListViewModel)
    {
        GraphListViewModel = graphListViewModel ?? throw new ArgumentNullException(nameof(graphListViewModel));
    }
    public GraphListViewModel GraphListViewModel { get; }
    public string CurrentDate => DateTime.Now.ToString("MMMM yyyy, dd");
}
