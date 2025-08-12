namespace ruby_plotter.app.Contracts.Interfaces;

/// <summary>
/// This interface introduces methods and functions to implement in the serializer service.
/// </summary>
public interface ISerializerService
{
    /// <summary>
    /// This function serializes the object's data to a file.
    /// </summary>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <param name="data">The object to serialize</param>
    /// <param name="fileName">The file name of the object serialized</param>
    /// <returns>
    /// <c>true</c> - the object serialized sucessfully;
    /// <c>false</c> - the object serialization failed.
    /// </returns>
    bool SerializeToFile<T>(T data, string fileName);

    /// <summary>
    /// This function deserializes the data from a file to an object.
    /// </summary>
    /// <typeparam name="T">Type of the object to serialize</typeparam>
    /// <param name="fileName">The file name of the object serialized</param>
    /// <returns>
    /// This function returns the object of the type T if the deserialization was successful;
    /// </returns>
    T? DeserializeFromFile<T>(string fileName) where T : class;
}
