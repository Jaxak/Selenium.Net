using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Controls;
using SeleniumTestCore.Dependencies;
using SeleniumTestCore.Page;

namespace SeleniumTestCore;

/// <summary>
/// Предоставляет методы расширения для настройки контейнера зависимостей (DI)
/// с необходимыми сервисами для работы с Selenium WebDriver и Page Object Model.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует все базовые сервисы, необходимые для работы с Selenium WebDriver.
    /// Включает регистрацию навигации, фабрик страниц и контролов, провайдера браузера,
    /// фабрик зависимостей и настройку Chrome браузера по умолчанию.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей</param>
    /// <returns>Обновленная коллекция сервисов для цепочки вызовов</returns>
    public static IServiceCollection AddSelenium(this IServiceCollection services)
        => services
            .AddScoped<Navigation>()
            .AddScoped<IPageFactory, PageFactory>()
            .AddScoped<IControlFactory, ControlFactory>()
            .AddScoped<IBrowserGetter, DefaultBrowserProvider>()
            .AddScoped<IDependenciesFactory, DependenciesFactory>()
            .AddScoped<IDependenciesFilter, DefaultDependenciesFilter>()
            .UseChrome<ChromeFactory, DefaultChromeOptionsProvider>();

    /// <summary>
    /// Настраивает использование Chrome браузера с указанными фабрикой и провайдером опций.
    /// Регистрирует фабрику браузера и провайдера настроек Chrome как singleton сервисы.
    /// </summary>
    /// <typeparam name="TFactory">Тип фабрики для создания экземпляров Chrome браузера</typeparam>
    /// <typeparam name="TOptions">Тип провайдера настроек Chrome браузера</typeparam>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей</param>
    /// <returns>Обновленная коллекция сервисов для цепочки вызовов</returns>
    public static IServiceCollection UseChrome<TFactory, TOptions>(this IServiceCollection services)
        where TFactory : class, IBrowserFactory
        where TOptions : class, IBrowserOptionsGetter<ChromeOptions>
        => services
            .AddSingleton<IBrowserFactory, TFactory>()
            .AddSingleton<IBrowserOptionsGetter<ChromeOptions>, TOptions>();
}
