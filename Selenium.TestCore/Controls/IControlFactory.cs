using OpenQA.Selenium;
using Selenium.POM.Abstractions;

namespace SeleniumTestCore.Controls;

public interface IControlFactory : IControlFactory<IWebElement>
{
    public TControl Create<TControl>(IWrapper<IWebDriver> page, By by)
        where TControl : IWrapper<IWebElement>;

    public TControl Create<TControl>(IWebDriver page, By by)
        where TControl : IWrapper<IWebElement>;

    public TControl Create<TControl>(IWrapper<IWebDriver> page, string dataTestId)
        where TControl : IWrapper<IWebElement>;
}