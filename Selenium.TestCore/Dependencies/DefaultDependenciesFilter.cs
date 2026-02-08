using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Dependencies;

public class DefaultDependenciesFilter : IDependenciesFilter
{
    public IEnumerable<ParameterInfo> Apply(IEnumerable<ParameterInfo> getParameters)
    {
        return getParameters
            .Where(x => x.ParameterType != typeof(IWebDriver))
            .Where(x => x.ParameterType != typeof(IWebElement))
            .Where(x => x.ParameterType != typeof(ISearchContext))
            .Where(x => x.ParameterType != typeof(Task<IWebDriver>))
            .Where(x => x.ParameterType != typeof(Task<IWebElement>))
            .Where(x => x.ParameterType != typeof(Task<ISearchContext>))
            .Where(x => x.ParameterType != typeof(Func<IWebDriver>))
            .Where(x => x.ParameterType != typeof(Func<IWebElement>))
            .Where(x => x.ParameterType != typeof(Func<ISearchContext>))
            .Where(x => x.ParameterType != typeof(Func<Task<IWebDriver>>))
            .Where(x => x.ParameterType != typeof(Func<Task<IWebElement>>))
            .Where(x => x.ParameterType != typeof(Func<Task<ISearchContext>>));
    }
}