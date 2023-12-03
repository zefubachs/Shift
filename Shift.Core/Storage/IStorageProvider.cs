namespace Shift.Storage;
public interface IStorageProvider
{
    string Name { get; }

    IStorageSession Create(Dictionary<string, string> arguments);
}
