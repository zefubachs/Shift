using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Shift.Cli.Infrastructure;
public class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection services;

    public TypeRegistrar(IServiceCollection services)
    {
        this.services = services;
    }

    public void Register(Type service, Type implementation)
    {
        services.AddTransient(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        services.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        services.AddSingleton(service, (s) => factory());
    }

    public ITypeResolver Build()
    {
        var serviceProvider = services.BuildServiceProvider();
        return new TypeResolver(serviceProvider);
    }
}
