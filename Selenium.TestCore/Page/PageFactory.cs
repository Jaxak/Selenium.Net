using System;
using System.Linq;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Dependencies;

namespace SeleniumTestCore.Page;

public class PageFactory(IDependenciesFactory dependenciesFactory) : IPageFactory
{
    public TPage Create<TPage>(IWebDriver page)
        where TPage : IWrapper<IWebDriver>
        => Create<TPage>(() => page);

    public TPage Create<TPage>(Func<IWebDriver> getPage)
        where TPage : IWrapper<IWebDriver>
    {
        var page = getPage();
        var dependency = dependenciesFactory.CreateDependency(typeof(TPage));
        return (TPage)Activator.CreateInstance(typeof(TPage), new[] { page }.Concat(dependency).ToArray())!;
    }
}