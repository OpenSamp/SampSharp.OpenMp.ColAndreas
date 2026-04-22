using Microsoft.Extensions.DependencyInjection;
using SampSharp.ColAndreas.Entities.Services;

// ReSharper disable once CheckNamespace — keep the extension visible without an extra using.
namespace SampSharp.Entities;

/// <summary>
/// DI registration for the ColAndreas wrapper. Adds <see cref="IColAndreasService"/>
/// (singleton) so any system / command can ask for it via constructor injection.
/// </summary>
public static class ServiceCollectionColAndreasExtensions
{
    /// <summary>Registers the ColAndreas managed service. Idempotent.</summary>
    public static IServiceCollection AddColAndreas(this IServiceCollection services)
    {
        services.AddSingleton<IColAndreasService, ColAndreasService>();
        return services;
    }
}
