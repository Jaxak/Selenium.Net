using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace SeleniumTests.Infra;

/// <summary>
/// Кэш для управления жизненным циклом ServiceProvider и Scope в контексте параллельного выполнения тестов.
/// Обеспечивает изоляцию зависимостей между тестами через отдельные Scope для каждого теста,
/// при этом используя единый ServiceProvider для всех тестов класса.
/// Гарантирует корректное освобождение ресурсов после каждого теста и глобальную очистку после всех тестов.
/// </summary>
public class ServiceProviderCache
{
    /// <summary>
    /// Глобальный кэш всех ServiceProvider, созданных в процессе выполнения тестов.
    /// Используется для финальной очистки всех ресурсов после завершения тестовой сессии.
    /// </summary>
    private static readonly ConcurrentBag<IServiceProvider> ProvidersGlobalCache = [];
    
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// Кэш Scope для каждого теста, где ключ - уникальный ID теста из TestContext.
    /// Обеспечивает изоляцию зависимостей между параллельно выполняющимися тестами.
    /// </summary>
    private readonly ConcurrentDictionary<string, IServiceScope> _serviceScopeCache = new();

    /// <summary>
    /// Создает экземпляр кэша с построенным ServiceProvider из коллекции сервисов.
    /// Добавляет созданный ServiceProvider в глобальный кэш для последующей очистки.
    /// </summary>
    /// <param name="services">Коллекция сервисов для построения ServiceProvider</param>
    public ServiceProviderCache(IServiceCollection services)
    {
        _serviceProvider = services.BuildServiceProvider();
        ProvidersGlobalCache.Add(_serviceProvider);
    }

    /// <summary>
    /// Получает или создает ServiceProvider для текущего теста.
    /// Каждый тест получает свой собственный Scope для изоляции зависимостей.
    /// Использует ID теста из TestContext как ключ для кэширования.
    /// </summary>
    /// <returns>ServiceProvider в контексте Scope текущего теста</returns>
    public IServiceProvider GetOrCreate()
        => _serviceScopeCache.GetOrAdd(
                TestContext.CurrentContext.Test.ID,
                _ => _serviceProvider.CreateScope()
            )
            .ServiceProvider;

    /// <summary>
    /// Освобождает Scope текущего теста.
    /// Вызывается в TearDown после завершения теста для очистки ресурсов конкретного теста
    /// (закрытие браузера, освобождение драйверов и т.д.).
    /// </summary>
    public void ScopeDispose()
    {
        if (_serviceScopeCache.TryRemove(TestContext.CurrentContext.Test.ID, out var scope))
        {
            scope.Dispose();
        }
    }

    /// <summary>
    /// Глобально освобождает все ServiceProvider, созданные в процессе тестирования.
    /// Вызывается один раз после завершения всех тестов в сборке.
    /// Асинхронно освобождает все ресурсы, реализующие IAsyncDisposable (браузеры, драйверы).
    /// </summary>
    public static async Task GlobalDisposeAsync()
    {
        foreach (var providers in ProvidersGlobalCache.OfType<IAsyncDisposable>())
        {
            await providers.DisposeAsync();
        }
    }
}
