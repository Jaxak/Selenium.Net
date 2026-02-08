using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Browser;

namespace SeleniumTestCore.Page;

public class Navigation(IBrowserGetter pageGetter, IPageFactory pageObjectsFactory)
{
    private readonly Lazy<Task<IWebDriver>> _page = new(pageGetter.GetAsync);

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

    public async Task<IWebDriver> GoToUrlAsync(string url)
    {
        var browserPage = await _page.Value;
        var pageUrl = url.TrimEnd('/');
        await browserPage.Navigate().GoToUrlAsync(pageUrl);
        return browserPage;
    }
}