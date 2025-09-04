using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ruby_plotter.app.Contracts.Interfaces;
using ruby_plotter.app.Contracts.Options;
using ruby_plotter.app.Services;
using ruby_plotter.app.ViewModel;
using System.Windows;

namespace ruby_plotter.app;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureApp(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureApp(ServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        services.AddOptions<AppDefaultSettings>()
            .Bind(config.GetSection(nameof(AppDefaultSettings)));

        services.AddOptions<TrigonometricPlotterSettings>()
            .Bind(config.GetSection(nameof(AppDefaultSettings))
                .GetSection(nameof(TrigonometricPlotterSettings)));
        services.AddOptions<SinCardinalPlotterSettings>()
            .Bind(config.GetSection(nameof(AppDefaultSettings))
                .GetSection(nameof(SinCardinalPlotterSettings)));

        services.AddTransient<MainWindow>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<GraphListViewModel>();
        services.AddTransient<ISerializerService, SerializerService>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}

