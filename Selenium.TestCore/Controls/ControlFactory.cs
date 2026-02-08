using System;
using System.Linq;
using OpenQA.Selenium;
using Selenium.POM.Abstractions;
using SeleniumTestCore.Dependencies;

namespace SeleniumTestCore.Controls;

/// <summary>
/// Фабрика для создания контролов (элементов управления) на веб-странице.
/// Реализует паттерн Factory для создания типизированных контролов из IWebElement с автоматическим разрешением их зависимостей.
/// Поддерживает различные способы поиска элементов: по data-test-id атрибуту, по CSS селектору (By) и из готовых элементов.
/// </summary>
public class ControlFactory(IDependenciesFactory dependenciesFactory) : IControlFactory
{
    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> путем поиска элемента по data-test-id атрибуту внутри родительского элемента.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="wrapper">Родительский элемент, внутри которого будет осуществляться поиск</param>
    /// <param name="dataTestId">Значение атрибута data-test-id для поиска элемента</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(IWrapper<IWebElement> wrapper, string dataTestId)
        where TControl : IWrapper<IWebElement>
        => Create<TControl>(() => wrapper.WrappedItem.FindElement(By.CssSelector($"[data-test-id='{dataTestId}']")));

    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> из готового IWebElement.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="item">Готовый веб-элемент для обертывания в контрол</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(IWebElement item)
        where TControl : IWrapper<IWebElement>
        => Create<TControl>(() => item);

    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> используя функцию для получения IWebElement.
    /// Основной метод создания контролов, который выполняет получение элемента, разрешение зависимостей через DI
    /// и создание экземпляра контрола с помощью рефлексии.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="getItem">Функция для получения веб-элемента (выполняется при создании контрола)</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(Func<IWebElement> getItem)
        where TControl : IWrapper<IWebElement>
    {
        var control = getItem();
        var dependency = dependenciesFactory.CreateDependency(typeof(TControl));
        return (TControl)Activator.CreateInstance(typeof(TControl), new[] { control }.Concat(dependency).ToArray())!;
    }

    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> путем поиска элемента по CSS селектору (By) на странице.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">Обертка над страницей (IWebDriver), на которой осуществляется поиск</param>
    /// <param name="by">Селектор для поиска элемента (By.Id, By.CssSelector и т.д.)</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(IWrapper<IWebDriver> page, By by) 
        where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(() => page.WrappedItem.FindElement(by));

    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> путем поиска элемента по CSS селектору (By) на странице.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">Страница (IWebDriver), на которой осуществляется поиск</param>
    /// <param name="by">Селектор для поиска элемента (By.Id, By.CssSelector и т.д.)</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(IWebDriver page, By by) where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(() => page.FindElement(by));

    /// <summary>
    /// Создает контрол типа <typeparamref name="TControl"/> путем поиска элемента по data-test-id атрибуту на странице.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;IWebElement&gt;</typeparam>
    /// <param name="page">Обертка над страницей (IWebDriver), на которой осуществляется поиск</param>
    /// <param name="dataTestId">Значение атрибута data-test-id для поиска элемента</param>
    /// <returns>Созданный экземпляр контрола с разрешенными зависимостями</returns>
    public TControl Create<TControl>(IWrapper<IWebDriver> page, string dataTestId) where TControl : IWrapper<IWebElement>
        =>  Create<TControl>(page, By.CssSelector($"[data-test-id='{dataTestId}']"));
}
