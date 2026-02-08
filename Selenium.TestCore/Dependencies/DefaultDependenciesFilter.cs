using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Dependencies;

/// <summary>
/// Фильтр зависимостей по умолчанию, исключающий стандартные типы Selenium из автоматического разрешения.
/// Отфильтровывает параметры конструктора, которые относятся к IWebDriver, IWebElement и ISearchContext,
/// включая их обертки в Task и Func, чтобы предотвратить их разрешение через контейнер зависимостей.
/// </summary>
public class DefaultDependenciesFilter : IDependenciesFilter
{
    /// <summary>
    /// Применяет фильтр к параметрам конструктора, исключая стандартные типы Selenium.
    /// Фильтрует IWebDriver, IWebElement, ISearchContext и их обертки в Task и Func.
    /// </summary>
    /// <param name="getParameters">Коллекция параметров конструктора для фильтрации</param>
    /// <returns>Отфильтрованная коллекция параметров, которые должны быть разрешены через DI</returns>
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
