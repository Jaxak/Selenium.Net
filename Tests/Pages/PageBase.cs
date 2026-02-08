using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Pages;

/// <summary>
/// Базовый абстрактный класс для всех объектов страниц (Page Objects) в проекте.
/// Предоставляет основную функциональность: доступ к WebDriver и ControlFactory.
/// Реализует интерфейс IWrapper&lt;IWebDriver&gt; для инкапсуляции драйвера.
/// Все конкретные Page Objects должны наследоваться от этого класса.
/// </summary>
/// <param name="webDriver">WebDriver для взаимодействия с браузером и страницей</param>
/// <param name="controlFactory">Фабрика для создания контролов на странице</param>
public abstract class PageBase(IWebDriver webDriver, IControlFactory controlFactory) : IWrapper<IWebDriver>
{
    /// <summary>
    /// Получает фабрику контролов для создания элементов управления на странице.
    /// Используется наследниками для определения свойств-контролов.
    /// </summary>
    protected IControlFactory ControlFactory { get; } = controlFactory;
    
    /// <summary>
    /// Получает обернутый WebDriver.
    /// Предоставляет прямой доступ к драйверу для выполнения низкоуровневых операций.
    /// </summary>
    public IWebDriver WrappedItem { get; } = webDriver;
}
