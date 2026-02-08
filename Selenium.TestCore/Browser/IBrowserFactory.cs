using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Интерфейс фабрики для создания экземпляров браузера (WebDriver).
/// Определяет контракт для реализации различных браузерных фабрик (Chrome, Firefox, Edge и т.д.).
/// </summary>
public interface IBrowserFactory
{
    /// <summary>
    /// Асинхронно создает новый экземпляр браузера.
    /// </summary>
    /// <returns>Задача, возвращающая инициализированный экземпляр IWebDriver</returns>
    Task<IWebDriver> CreateAsync();
}
