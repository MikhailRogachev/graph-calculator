using System.ComponentModel;

namespace ruby_plotter.tests.Utilites.Helpers;

public class ModelViewTester
{
    private readonly IList<string> _propertiesChanged = new List<string>();
    public void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        _propertiesChanged.Add(e.PropertyName!);
    }
    public IList<string> PropertiesChanged => _propertiesChanged;
}
