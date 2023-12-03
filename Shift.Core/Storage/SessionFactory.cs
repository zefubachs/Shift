namespace Shift.Storage;
public class SessionFactory(ShiftOptions options, IEnumerable<IStorageProvider> providers)
{
    public IStorageSession CreateSession()
    {
        var provider = providers.FirstOrDefault(x => string.Equals(x.Name, options.Store.Provider, StringComparison.OrdinalIgnoreCase));
        if (provider is null)
            throw new InvalidOperationException($"Unknown provider '{options.Store.Provider}' in configuration");

        return provider.Create(options.Store.Args);
    }
}
