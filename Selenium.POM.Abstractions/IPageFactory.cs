using System;

namespace Selenium.POM.Abstractions;

/// <summary>
/// Универсальный интерфейс фабрики для создания объектов страниц (Page Objects).
/// Предоставляет методы для инициализации страниц из готовых драйверов или фабричных функций.
/// Используется для реализации паттерна Page Object Model в автоматизации тестирования.
/// </summary>
/// <typeparam name="TWrappedItem">Тип обернутого элемента (например, IWebDriver для страниц Selenium)</typeparam>
public interface IPageFactory<in TWrappedItem>
{
    /// <summary>
    /// Создает экземпляр страницы из готового объекта драйвера/страницы.
    /// Используется когда драйвер уже инициализирован и нужно создать Page Object.
    /// </summary>
    /// <typeparam name="TPage">Тип создаваемой страницы, должен реализовывать IWrapper&lt;TWrappedItem&gt;</typeparam>
    /// <param name="page">Обернутый объект страницы/драйвера для инициализации Page Object</param>
    /// <returns>Экземпляр страницы типа <typeparamref name="TPage"/></returns>
    TPage Create<TPage>(TWrappedItem page)
        where TPage : IWrapper<TWrappedItem>;
    
    /// <summary>
    /// Создает экземпляр страницы из фабричной функции, предоставляющей драйвер.
    /// Позволяет отложить получение драйвера до момента фактического использования страницы.
    /// </summary>
    /// <typeparam name="TPage">Тип создаваемой страницы, должен реализовывать IWrapper&lt;TWrappedItem&gt;</typeparam>
    /// <param name="getPage">Функция для получения обернутого объекта страницы/драйвера</param>
    /// <returns>Экземпляр страницы типа <typeparamref name="TPage"/></returns>
    TPage Create<TPage>(Func<TWrappedItem> getPage)
        where TPage : IWrapper<TWrappedItem>;
}
