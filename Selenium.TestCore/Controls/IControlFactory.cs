using OpenQA.Selenium;
using Selenium.POM.Abstractions;

namespace SeleniumTestCore.Controls;

/// <summary>
/// Специализированный интерфейс фабрики для создания Selenium контролов (элементов управления).
/// Расширяет базовый IControlFactory&lt;IWebElement&gt; дополнительными методами для работы с Selenium WebDriver:
/// поддержка поиска по By локаторам и data-test-id атрибутам в контексте страницы или драйвера.
/// </summary>
public interface IControlFactory : IControlFactory<IWebElement>
{
    /// <summary>
    /// Создает контрол, найденный по указанному локатору внутри родительской страницы-обёртки.
    /// Используется для поиска элементов с помощью стандартных Selenium локаторов (By.Id, By.XPath и т.д.)
    /// в пределах определённого Page Object.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">Родительская страница-обёртка, внутри которой выполняется поиск элемента</param>
    /// <param name="by">Selenium локатор для поиска элемента (например, By.Id, By.CssSelector)</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    public TControl Create<TControl>(IWrapper<IWebDriver> page, By by)
        where TControl : IWrapper<IWebElement>;

    /// <summary>
    /// Создает контрол, найденный по указанному локатору непосредственно в драйвере.
    /// Используется для прямого поиска элементов на странице через WebDriver
    /// без промежуточной обёртки Page Object.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">WebDriver для поиска элемента</param>
    /// <param name="by">Selenium локатор для поиска элемента (например, By.Id, By.CssSelector)</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    public TControl Create<TControl>(IWebDriver page, By by)
        where TControl : IWrapper<IWebElement>;

    /// <summary>
    /// Создает контрол по значению data-test-id атрибута внутри родительской страницы-обёртки.
    /// Предоставляет удобный способ поиска элементов по тестовым идентификаторам,
    /// автоматически формируя CSS селектор [data-test-id='значение'].
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">Родительская страница-обёртка, внутри которой выполняется поиск элемента</param>
    /// <param name="dataTestId">Значение атрибута data-test-id для поиска элемента</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    public TControl Create<TControl>(IWrapper<IWebDriver> page, string dataTestId)
        where TControl : IWrapper<IWebElement>;
}
