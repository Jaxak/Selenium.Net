using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace SeleniumTests.Infra;

public class ServiceProviderCache
{
    private static readonly ConcurrentBag<IServiceProvider> ProvidersGlobalCache = [];
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<string, IServiceScope> _serviceScopeCache = new();

    public ServiceProviderCache(IServiceCollection services)
    {
        _serviceProvider = services.BuildServiceProvider();
        ProvidersGlobalCache.Add(_serviceProvider);
    }

    public IServiceProvider GetOrCreate()
        => _serviceScopeCache.GetOrAdd(
                TestContext.CurrentContext.Test.ID,
                _ => _serviceProvider.CreateScope()
            )
            .ServiceProvider;

    public void ScopeDispose()
    {
        if (_serviceScopeCache.TryRemove(TestContext.CurrentContext.Test.ID, out var scope))
        {
            scope.Dispose();
        }
    }

    public static async Task GlobalDisposeAsync()
    {
        foreach (var providers in ProvidersGlobalCache.OfType<IAsyncDisposable>())
        {
            await providers.DisposeAsync();
        }
    }
}