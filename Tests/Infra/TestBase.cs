using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SeleniumTestCore;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Page;

namespace SeleniumTests.Infra;

[Parallelizable(ParallelScope.All)]
public abstract class TestBase
{
    protected TestBase() : this(_ => { })
    {
    }

    protected TestBase(Action<IServiceCollection> configure)
    {
        var services = new ServiceCollection()
            .AddSelenium()
            .UseChrome<ChromeFactory, DefaultChromeOptionsProvider>();
        configure(services);
        _serviceProviderCache =  new ServiceProviderCache(services);
    }

    private readonly ServiceProviderCache _serviceProviderCache;

    protected IServiceProvider ServiceProvider
        => _serviceProviderCache.GetOrCreate();

    protected TService Get<TService>() where TService : notnull 
        => ServiceProvider.GetRequiredService<TService>();
    
    protected Navigation Navigation 
        => Get<Navigation>(); 

    [TearDown]
    public void ScopeDispose()
        => _serviceProviderCache.ScopeDispose();
}