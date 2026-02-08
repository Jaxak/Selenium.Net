namespace Selenium.POM.Abstractions;

/// <summary>
/// Интерфейс-обертка, предоставляющий доступ к обернутому элементу типа <typeparamref name="TItem"/>.
/// Используется для реализации паттерна Page Object Model, позволяя контролам и страницам
/// инкапсулировать доступ к нижележащим Selenium элементам.
/// </summary>
/// <typeparam name="TItem">Тип обернутого элемента (например, IWebElement или IWebDriver)</typeparam>
public interface IWrapper<out TItem>
{
    /// <summary>
    /// Получает обернутый элемент.
    /// </summary>
    TItem WrappedItem { get; }
}
