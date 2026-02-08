using System;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Провайдер браузера по умолчанию, обеспечивающий ленивую инициализацию и управление жизненным циклом браузера.
/// Создает единственный экземпляр браузера при первом обращении и корректно освобождает ресурсы при завершении.
/// Реализует паттерн Lazy initialization для отложенного создания браузера.
/// </summary>
public class DefaultBrowserProvider(IBrowserFactory browserFactory) 
    : IBrowserGetter, IAsyncDisposable, IDisposable
{
    private readonly Lazy<Task<IWebDriver>> _driver = new(browserFactory.CreateAsync);

    /// <summary>
    /// Асинхронно получает экземпляр браузера. При первом вызове создает новый экземпляр,
    /// при последующих вызовах возвращает уже созданный.
    /// </summary>
    /// <returns>Задача, возвращающая экземпляр IWebDriver</returns>
    public Task<IWebDriver> GetAsync()
        => _driver.Value;

    /// <summary>
    /// Асинхронно освобождает ресурсы браузера. Закрывает и корректно завершает работу WebDriver,
    /// если он был создан.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_driver.IsValueCreated)
        {
            var driver = await _driver.Value;
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }

    /// <summary>
    /// Синхронно освобождает ресурсы браузера. Выполняет асинхронное освобождение синхронно.
    /// </summary>
    public void Dispose()
        => DisposeAsync().GetAwaiter().GetResult();
}
