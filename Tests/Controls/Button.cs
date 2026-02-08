using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Page;

namespace SeleniumTests.Controls;

/// <summary>
/// Контрол кнопки, представляющий интерактивный элемент на веб-странице.
/// Расширяет базовую функциональность ControlBase добавлением метода для клика с автоматическим переходом на новую страницу.
/// Используется для навигации между страницами после выполнения действия.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement, представляющий кнопку на странице</param>
/// <param name="pageFactory">Фабрика для создания объектов страниц после перехода</param>
/// <param name="browserGetter">Провайдер для получения экземпляра браузера</param>
public class Button(IWebElement webElement, IPageFactory pageFactory, IBrowserGetter browserGetter) : ControlBase(webElement)
{
    /// <summary>
    /// Выполняет клик по кнопке и создает объект новой страницы типа <typeparamref name="TPage"/>.
    /// Используется когда клик по кнопке приводит к переходу на другую страницу или открытию новой вкладки.
    /// Автоматически получает текущий драйвер и создает соответствующий Page Object.
    /// </summary>
    /// <typeparam name="TPage">Тип страницы, на которую выполняется переход</typeparam>
    /// <returns>Задача, возвращающая созданный объект страницы типа <typeparamref name="TPage"/></returns>
    public async Task<TPage> ClickAndOpenAsync<TPage>() 
        where TPage : IWrapper<IWebDriver>
    {
        WrappedItem.Click();
        var driver = await browserGetter.GetAsync();
        return pageFactory.Create<TPage>(driver);
    }    
}
