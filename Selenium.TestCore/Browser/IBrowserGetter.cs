using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Интерфейс провайдера для получения экземпляра браузера.
/// Используется для доступа к уже созданному или для ленивого создания экземпляра WebDriver.
/// </summary>
public interface IBrowserGetter
{
    /// <summary>
    /// Асинхронно получает экземпляр браузера.
    /// </summary>
    /// <returns>Задача, возвращающая экземпляр IWebDriver</returns>
    Task<IWebDriver> GetAsync();
}
