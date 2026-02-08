using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Browser;
using SeleniumTestCore.Page;

namespace SeleniumTests.Controls;

public class Button(IWebElement webElement, IPageFactory pageFactory, IBrowserGetter browserGetter) : ControlBase(webElement)
{
    public async Task<TPage> ClickAndOpenAsync<TPage>() 
        where TPage : IWrapper<IWebDriver>
    {
        WrappedItem.Click();
        var driver = await browserGetter.GetAsync();
        return pageFactory.Create<TPage>(driver);
    }    
}