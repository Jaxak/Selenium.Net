using System;

namespace Selenium.POM.Abstractions;

/// <summary>
/// Универсальный интерфейс фабрики для создания контролов (элементов управления) Page Object Model.
/// Предоставляет методы для создания контролов из различных источников: готовых элементов, обёрток или фабричных функций.
/// </summary>
/// <typeparam name="TWrappedItem">Тип обернутого элемента (например, IWebElement для Selenium контролов)</typeparam>
public interface IControlFactory<in TWrappedItem>
{
    /// <summary>
    /// Создает контрол по значению data-test-id атрибута внутри родительского элемента-обёртки.
    /// Используется для поиска элементов по тестовому идентификатору в пределах определённой области страницы.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;TWrappedItem&gt;</typeparam>
    /// <param name="wrapper">Родительская обёртка, внутри которой выполняется поиск элемента</param>
    /// <param name="dataTestId">Значение атрибута data-test-id для поиска элемента</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    TControl Create<TControl>(IWrapper<TWrappedItem> wrapper, string dataTestId) 
        where TControl : IWrapper<TWrappedItem>;
    
    /// <summary>
    /// Создает контрол из фабричной функции, предоставляющей элемент.
    /// Позволяет отложить получение элемента до момента его фактического использования.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;TWrappedItem&gt;</typeparam>
    /// <param name="getItem">Функция для получения обернутого элемента</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    TControl Create<TControl>(Func<TWrappedItem> getItem)
        where TControl : IWrapper<TWrappedItem>;    
    
    /// <summary>
    /// Создает контрол из уже существующего элемента.
    /// Используется когда элемент уже получен и нужно обернуть его в контрол.
    /// </summary>
    /// <typeparam name="TControl">Тип создаваемого контрола, должен реализовывать IWrapper&lt;TWrappedItem&gt;</typeparam>
    /// <param name="item">Обернутый элемент для создания контрола</param>
    /// <returns>Экземпляр контрола типа <typeparamref name="TControl"/></returns>
    TControl Create<TControl>(TWrappedItem item)
        where TControl : IWrapper<TWrappedItem>;
}
