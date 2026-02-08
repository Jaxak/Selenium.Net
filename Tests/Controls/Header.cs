using OpenQA.Selenium;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Controls;

/// <summary>
/// Контрол шапки сайта (Header), содержащий основные элементы навигации.
/// Предоставляет доступ к кнопке меню и другим элементам верхней панели страницы.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement, представляющий шапку сайта</param>
/// <param name="controlFactory">Фабрика для создания вложенных контролов внутри шапки</param>
public class Header(IWebElement webElement, IControlFactory controlFactory) : ControlBase(webElement)
{
    /// <summary>
    /// Получает кнопку переключения меню (гамбургер-меню).
    /// Кнопка используется для открытия/закрытия бокового навигационного меню.
    /// Элемент находится по CSS селектору с атрибутом aria-label='Toggle menu'.
    /// </summary>
    public Button Menu
        => controlFactory.Create<Button>(() => WrappedItem.FindElement(By.CssSelector("[aria-label='Toggle menu']")));
}
