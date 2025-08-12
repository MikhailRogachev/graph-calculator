using Microsoft.Extensions.Options;
using ruby_plotter.app.Contracts.Interfaces;
using ruby_plotter.app.Contracts.Options;
using System.IO;

namespace ruby_plotter.app.Services;

/// <summary>
/// This service provides methods for serializing and deserializing objects to and from files.
/// </summary>
public class SerializerService : ISerializerService
{
    private readonly AppDefaultSettings _appSettings;

    public SerializerService(IOptions<AppDefaultSettings> options)
    {
        _appSettings = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
    }

    /// <summary>
    /// This function retrieves the file path from the application settings.
    /// </summary>
    /// <returns>
    ///     The full file path where the data is stored.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     This exception is thrown if the default file path is null or empty.
    /// </exception>
    private string GetFilePath()
    {
        var baseDirectory = AppContext.BaseDirectory;

        if (string.IsNullOrWhiteSpace(_appSettings.DefaultFilePath))
        {
            throw new ArgumentException("Default file path cannot be null or empty.", nameof(_appSettings.DefaultFilePath));
        }

        return Path.Combine(baseDirectory, _appSettings.DefaultFilePath!)!;
    }

    /// <inheritdoc cref="ISerializerService.DeserializeFromFile{T}(string)"/>/>
    /// <returns>
    ///     The object type T deserialized from the specified file.
    ///     In case the file with data doesn't exist, it returns null.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     This exception is thrown if the file cannot be read or deserialized.
    /// </exception>
    public T? DeserializeFromFile<T>(string fileName) where T : class
    {
        var path = GetFilePath();
        var fileFullName = Path.Combine(path, fileName);

        if (!File.Exists(fileFullName))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(fileFullName);
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to deserialize from file: {fileFullName}", ex);
        }
    }

    /// <inheritdoc cref="ISerializerService.SerializeToFile{T}(T, string)"/>/>
    /// <exception cref="InvalidOperationException">
    ///     This exception is thrown if the data cannot be serialized to the file.
    /// </exception>
    public bool SerializeToFile<T>(T data, string fileName)
    {
        var path = GetFilePath();
        var fileFullName = Path.Combine(path, fileName);

        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(fileFullName, json);
            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to serialize to file: {fileFullName}", ex);
        }
    }
}
