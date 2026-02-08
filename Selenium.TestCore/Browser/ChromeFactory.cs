using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Фабрика для создания экземпляров Chrome браузера с настроенными опциями.
/// Реализует паттерн Factory для инициализации ChromeDriver с заданной конфигурацией.
/// </summary>
public class ChromeFactory(IBrowserOptionsGetter<ChromeOptions> optionsProvider) : IBrowserFactory
{
    /// <summary>
    /// Асинхронно создает новый экземпляр Chrome браузера с настроенными опциями.
    /// </summary>
    /// <returns>Задача, возвращающая инициализированный экземпляр IWebDriver для Chrome</returns>
    public async Task<IWebDriver> CreateAsync()
    {
        var options = await optionsProvider.GetOptionsAsync();
        return new ChromeDriver(options);
    }
}
