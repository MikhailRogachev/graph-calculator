namespace ruby_plotter.app.Contracts.Interfaces;

public interface ISerializerService
{
    bool SerializeToFile<T>(T data, string fileName);
    T? DeserializeFromFile<T>(string fileName) where T : class;
}
