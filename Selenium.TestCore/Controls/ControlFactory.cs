using System;
using System.Linq;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Dependencies;

namespace SeleniumTestCore.Controls;

public class ControlFactory(IDependenciesFactory dependenciesFactory) : IControlFactory
{
    public TControl Create<TControl>(IWrapper<IWebElement> wrapper, string dataTestId)
        where TControl : IWrapper<IWebElement>
        => Create<TControl>(() => wrapper.WrappedItem.FindElement(By.CssSelector($"[data-test-id='{dataTestId}']")));

    public TControl Create<TControl>(IWebElement item)
        where TControl : IWrapper<IWebElement>
        => Create<TControl>(() => item);

    public TControl Create<TControl>(Func<IWebElement> getItem)
        where TControl : IWrapper<IWebElement>
    {
        var control = getItem();
        var dependency = dependenciesFactory.CreateDependency(typeof(TControl));
        return (TControl)Activator.CreateInstance(typeof(TControl), new[] { control }.Concat(dependency).ToArray())!;
    }

    public TControl Create<TControl>(IWrapper<IWebDriver> page, By by) 
        where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(() => page.WrappedItem.FindElement(by));

    public TControl Create<TControl>(IWebDriver page, By by) where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(() => page.FindElement(by));

    public TControl Create<TControl>(IWrapper<IWebDriver> page, string dataTestId) where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(page, By.CssSelector($"[data-test-id='{dataTestId}']"));
}