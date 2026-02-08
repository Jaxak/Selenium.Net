using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Controls;
using SeleniumTestCore.Dependencies;
using SeleniumTestCore.Page;

namespace SeleniumTestCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSelenium(this IServiceCollection services)
        => services
            .AddScoped<Navigation>()
            .AddScoped<IPageFactory, PageFactory>()
            .AddScoped<IControlFactory, ControlFactory>()
            .AddScoped<IBrowserGetter, DefaultBrowserProvider>()
            .AddScoped<IDependenciesFactory, DependenciesFactory>()
            .AddScoped<IDependenciesFilter, DefaultDependenciesFilter>()
            .UseChrome<ChromeFactory, DefaultChromeOptionsProvider>();

    public static IServiceCollection UseChrome<TFactory, TOptions>(this IServiceCollection services)
        where TFactory : class, IBrowserFactory
        where TOptions : class, IBrowserOptionsGetter<ChromeOptions>
        => services
            .AddSingleton<IBrowserFactory, TFactory>()
            .AddSingleton<IBrowserOptionsGetter<ChromeOptions>, TOptions>();
}