using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Browser;

namespace SeleniumTestCore.Page;

/// <summary>
/// Сервис навигации для управления переходами между страницами в браузере.
/// Обеспечивает создание объектов страниц (Page Objects) и переход по URL с автоматическим ожиданием загрузки.
/// Реализует паттерн Lazy initialization для отложенного создания браузера.
/// </summary>
public class Navigation(IBrowserGetter pageGetter, IPageFactory pageObjectsFactory)
{
    private readonly Lazy<Task<IWebDriver>> _page = new(pageGetter.GetAsync);

    /// <summary>
    /// Асинхронно переходит на указанный URL и создает объект страницы типа <typeparamref name="TPage"/>.
    /// Если страница реализует интерфейс ILoadable, автоматически ожидает её полной загрузки.
    /// </summary>
    /// <typeparam name="TPage">Тип объекта страницы, наследующий IWrapper&lt;IWebDriver&gt;</typeparam>
    /// <param name="url">URL адрес для перехода</param>
    /// <returns>Задача, возвращающая созданный и загруженный объект страницы</returns>
    public async Task<TPage> GoToPageAsync<TPage>(string url)
        where TPage : notnull, IWrapper<IWebDriver>
    {
        var page = await GoToUrlAsync(url);
        var pageObject = pageObjectsFactory.Create<TPage>(page);
        if (pageObject is ILoadable loadable)
        {
            await loadable.WaitLoadAsync();
        }

        return pageObject;
    }

    /// <summary>
    /// Асинхронно переходит на указанный URL без создания объекта страницы.
    /// Возвращает экземпляр IWebDriver для прямого управления браузером.
    /// </summary>
    /// <param name="url">URL адрес для перехода (завершающий слеш будет удален)</param>
    /// <returns>Задача, возвращающая экземпляр IWebDriver после перехода на страницу</returns>
    public async Task<IWebDriver> GoToUrlAsync(string url)
    {
        var browserPage = await _page.Value;
        var pageUrl = url.TrimEnd('/');
        await browserPage.Navigate().GoToUrlAsync(pageUrl);
        return browserPage;
    }
}
