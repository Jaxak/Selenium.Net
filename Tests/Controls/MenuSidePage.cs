using OpenQA.Selenium;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Controls;

/// <summary>
/// Контрол бокового меню страницы, содержащий навигационные элементы и фильтры.
/// Предоставляет доступ к различным разделам и опциям боковой панели.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement, представляющий боковое меню</param>
/// <param name="controlFactory">Фабрика для создания вложенных контролов внутри меню</param>
public class MenuSidePage(IWebElement webElement, IControlFactory controlFactory) : ControlBase(webElement)
{
    /// <summary>
    /// Получает кнопку раздела "Что нового" в боковом меню.
    /// Используется для перехода к разделу с новостями и обновлениями.
    /// Элемент находится по атрибуту data-test-id="whats-new-menu-item".
    /// </summary>
    public Button NewsButton
        => controlFactory.Create<Button>(this, "whats-new-menu-item");
}
