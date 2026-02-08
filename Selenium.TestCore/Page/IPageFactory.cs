using OpenQA.Selenium;
using Selenium.POM.Abstractions;

namespace SeleniumTestCore.Page;

/// <summary>
/// Специализированный интерфейс фабрики для создания Page Objects на основе Selenium WebDriver.
/// Наследует универсальный IPageFactory&lt;IWebDriver&gt; и используется для создания страниц,
/// работающих с IWebDriver в качестве обернутого элемента.
/// Упрощает работу с Page Object Model, скрывая необходимость явного указания типа драйвера.
/// </summary>
public interface IPageFactory : IPageFactory<IWebDriver>
{
}
