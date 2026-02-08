using OpenQA.Selenium;
using Selenium.POM.Abstractions;

namespace SeleniumTests.Controls;

/// <summary>
/// Базовый абстрактный класс для всех контролов (элементов управления) в Page Object Model.
/// Предоставляет основную функциональность работы с Selenium WebElement:
/// обёртку элемента, базовые операции клика и проверки видимости.
/// Все конкретные контролы (Button, Input, TextLabel и т.д.) должны наследоваться от этого класса.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement для работы с DOM элементом</param>
public abstract class ControlBase(IWebElement webElement) : IWrapper<IWebElement>
{
    /// <summary>
    /// Получает обернутый Selenium WebElement.
    /// Предоставляет прямой доступ к нижележащему DOM элементу для выполнения операций.
    /// </summary>
    public IWebElement WrappedItem { get; } = webElement;
    
    /// <summary>
    /// Выполняет клик по элементу.
    /// Виртуальный метод, может быть переопределён в наследниках для добавления дополнительной логики
    /// (например, ожидания, логирования, обработки исключений).
    /// </summary>
    public virtual void Click() 
        => WrappedItem.Click();
    
    /// <summary>
    /// Получает значение, указывающее, виден ли элемент на странице.
    /// Проверяет CSS свойства видимости элемента (display, visibility, opacity).
    /// </summary>
    public bool IsVisible
        => WrappedItem.Displayed;
}
