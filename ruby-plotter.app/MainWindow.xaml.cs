using ruby_plotter.app.ViewModel;
using System.Windows;

namespace ruby_plotter.app;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();

        _viewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        DataContext = _viewModel;
    }
}