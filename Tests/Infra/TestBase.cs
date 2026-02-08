using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SeleniumTestCore;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Page;

namespace SeleniumTests.Infra;

/// <summary>
/// Базовый абстрактный класс для всех тестовых классов в проекте.
/// Предоставляет настроенный контейнер зависимостей (DI) с Selenium сервисами,
/// управление жизненным циклом ServiceProvider и Scope для каждого теста,
/// а также упрощенный доступ к сервисам через методы-помощники.
/// Поддерживает параллельное выполнение тестов на уровне всех тестов класса.
/// </summary>
[Parallelizable(ParallelScope.All)]
public abstract class TestBase
{
    /// <summary>
    /// Конструктор по умолчанию без дополнительной настройки сервисов.
    /// Использует стандартный набор Selenium сервисов с Chrome браузером.
    /// </summary>
    protected TestBase() : this(_ => { })
    {
    }

    /// <summary>
    /// Конструктор с возможностью дополнительной настройки контейнера зависимостей.
    /// Позволяет наследникам добавлять или переопределять сервисы для специфичных тестовых сценариев.
    /// </summary>
    /// <param name="configure">Действие для настройки коллекции сервисов</param>
    protected TestBase(Action<IServiceCollection> configure)
    {
        var services = new ServiceCollection()
            .AddSelenium()
            .UseChrome<ChromeFactory, DefaultChromeOptionsProvider>();
        configure(services);
        _serviceProviderCache =  new ServiceProviderCache(services);
    }

    private readonly ServiceProviderCache _serviceProviderCache;

    /// <summary>
    /// Получает ServiceProvider для текущего теста.
    /// Каждый тест получает свой изолированный Scope с зависимостями.
    /// </summary>
    protected IServiceProvider ServiceProvider
        => _serviceProviderCache.GetOrCreate();

    /// <summary>
    /// Получает сервис типа <typeparamref name="TService"/> из контейнера зависимостей.
    /// Выбрасывает исключение, если сервис не зарегистрирован.
    /// </summary>
    /// <typeparam name="TService">Тип запрашиваемого сервиса</typeparam>
    /// <returns>Экземпляр сервиса типа <typeparamref name="TService"/></returns>
    protected TService Get<TService>() where TService : notnull 
        => ServiceProvider.GetRequiredService<TService>();
    
    /// <summary>
    /// Получает сервис навигации для перехода между страницами.
    /// Упрощенный доступ к Navigation без необходимости явного вызова Get&lt;Navigation&gt;().
    /// </summary>
    protected Navigation Navigation 
        => Get<Navigation>(); 

    /// <summary>
    /// Освобождает Scope текущего теста после его завершения.
    /// Вызывается автоматически NUnit после каждого теста для очистки ресурсов.
    /// </summary>
    [TearDown]
    public void ScopeDispose()
        => _serviceProviderCache.ScopeDispose();
}
