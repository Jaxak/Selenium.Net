using System;
using System.Linq;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Dependencies;

namespace SeleniumTestCore.Page;

/// <summary>
/// Фабрика для создания объектов страниц (Page Objects) в паттерне Page Object Model.
/// Создает типизированные страницы из IWebDriver с автоматическим разрешением их зависимостей через контейнер DI.
/// </summary>
public class PageFactory(IDependenciesFactory dependenciesFactory) : IPageFactory
{
    /// <summary>
    /// Создает объект страницы типа <typeparamref name="TPage"/> из готового экземпляра IWebDriver.
    /// </summary>
    /// <typeparam name="TPage">Тип создаваемой страницы, должен реализовывать IWrapper&lt;IWebDriver&gt;</typeparam>
    /// <param name="page">Экземпляр IWebDriver, представляющий браузер на нужной странице</param>
    /// <returns>Созданный экземпляр объекта страницы с разрешенными зависимостями</returns>
    public TPage Create<TPage>(IWebDriver page)
        where TPage : IWrapper<IWebDriver>
        => Create<TPage>(() => page);

    /// <summary>
    /// Создает объект страницы типа <typeparamref name="TPage"/> используя функцию для получения IWebDriver.
    /// Выполняет получение драйвера, разрешение зависимостей через DI и создание экземпляра страницы с помощью рефлексии.
    /// </summary>
    /// <typeparam name="TPage">Тип создаваемой страницы, должен реализовывать IWrapper&lt;IWebDriver&gt;</typeparam>
    /// <param name="getPage">Функция для получения экземпляра IWebDriver (выполняется при создании страницы)</param>
    /// <returns>Созданный экземпляр объекта страницы с разрешенными зависимостями</returns>
    public TPage Create<TPage>(Func<IWebDriver> getPage)
        where TPage : IWrapper<IWebDriver>
    {
        var page = getPage();
        var dependency = dependenciesFactory.CreateDependency(typeof(TPage));
        return (TPage)Activator.CreateInstance(typeof(TPage), new[] { page }.Concat(dependency).ToArray())!;
    }
}
