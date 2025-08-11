namespace ruby_plotter.app.ViewModel;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(GraphListViewModel graphListViewModel)
    {
        GraphListViewModel = graphListViewModel ?? throw new ArgumentNullException(nameof(graphListViewModel));
    }
    public GraphListViewModel GraphListViewModel { get; }
    public string CurrentDate => DateTime.Now.ToString("MMMM yyyy, dd");
}
