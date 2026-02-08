using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTestCore.Controls;
using SeleniumTestCore.Page;
using SeleniumTests.Controls;

namespace SeleniumTests.Pages;

/// <summary>
/// Page Object для главной страницы сайта Habr.com.
/// Инкапсулирует структуру страницы и предоставляет доступ к основным элементам:
/// шапке сайта (Header) и боковому меню (MenuSidePage).
/// Реализует интерфейс ILoadable для обеспечения явного ожидания загрузки критичных элементов страницы.
/// </summary>
/// <param name="driver">WebDriver для взаимодействия со страницей</param>
/// <param name="controlFactory">Фабрика для создания контролов на странице</param>
public class HabrPage(IWebDriver driver, IControlFactory controlFactory)
    : PageBase(driver, controlFactory), ILoadable
{
    /// <summary>
    /// Получает контрол шапки сайта (Header).
    /// Содержит основные элементы навигации, логотип, поиск и меню пользователя.
    /// Элемент находится по атрибуту data-test-id="header".
    /// </summary>
    public Header Header
        => ControlFactory.Create<Header>(this, "header");
    
    /// <summary>
    /// Получает контрол бокового меню страницы (MenuSidePage).
    /// Содержит разделы навигации, фильтры и дополнительные опции.
    /// Элемент находится по атрибуту data-test-id="expanded-menu".
    /// </summary>
    public MenuSidePage MenuSidePage
        => ControlFactory.Create<MenuSidePage>(this, "expanded-menu");

    /// <summary>
    /// Асинхронно ожидает полной загрузки страницы.
    /// Проверяет, что меню в шапке стало активным и доступным для взаимодействия.
    /// Ожидание: до 1000 мс с интервалом проверки 100 мс.
    /// </summary>
    /// <returns>Задача, завершающаяся после загрузки критичных элементов страницы</returns>
    public Task WaitLoadAsync()
    {
        Assert.That(Header.Menu.WrappedItem.Enabled, Is.True.After(1000, 100));
        return Task.CompletedTask;
    }
}
