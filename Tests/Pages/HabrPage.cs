using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTestCore.Controls;
using SeleniumTestCore.Page;
using SeleniumTests.Controls;

namespace SeleniumTests.Pages;

public class HabrPage(IWebDriver driver, IControlFactory controlFactory)
    : PageBase(driver, controlFactory), ILoadable
{
    public Header Header
        => ControlFactory.Create<Header>(this, "header");
    
    public MenuSidePage MenuSidePage
        => ControlFactory.Create<MenuSidePage>(this, "expanded-menu");

    public Task WaitLoadAsync()
    {
        Assert.That(Header.Menu.WrappedItem.Enabled, Is.True.After(1000, 100));
        return Task.CompletedTask;
    }
}