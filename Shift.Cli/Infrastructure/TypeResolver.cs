using Spectre.Console.Cli;

namespace Shift.Cli.Infrastructure;
internal class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IServiceProvider serviceProvider;

    public TypeResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public object? Resolve(Type? type)
    {
        if (type is null)
            return null;

        return serviceProvider.GetService(type);
    }

    public void Dispose()
    {
        if (serviceProvider is IDisposable disposable)
            disposable.Dispose();
    }
}
