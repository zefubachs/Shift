using Shift;
using Shift.Storage;
using Shift.Storage.LiteDB;

namespace Microsoft.Extensions.DependencyInjection;
public static class ShiftServiceCollectionExtensions
{
    public static IServiceCollection AddShift(this IServiceCollection services)
        => AddShift(services, ShiftOptions.LoadUserDefault());

    public static IServiceCollection AddShift(this IServiceCollection services, ShiftOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<SessionFactory>();
        services.AddSingleton<IStorageProvider, LiteDBProvider>();
        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<SessionFactory>();
            return factory.CreateSession();
        });

        return services;
    }
}
