using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Pages;

public abstract class PageBase(IWebDriver webDriver, IControlFactory controlFactory) : IWrapper<IWebDriver>
{
    protected IControlFactory ControlFactory { get; } = controlFactory;
    public IWebDriver WrappedItem { get; } = webDriver;
}