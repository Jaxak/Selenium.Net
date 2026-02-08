using OpenQA.Selenium;

namespace SeleniumTests.Controls;

/// <summary>
/// Контрол поля ввода (Input), представляющий текстовое поле на веб-странице.
/// Расширяет базовую функциональность ControlBase добавлением метода для ввода текста.
/// Используется для заполнения форм, поиска и других операций ввода данных.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement, представляющий поле ввода</param>
public class Input(IWebElement webElement) : ControlBase(webElement)
{
    /// <summary>
    /// Отправляет текст в поле ввода.
    /// Виртуальный метод, может быть переопределён в наследниках для добавления дополнительной логики
    /// (например, предварительной очистки поля, валидации, логирования).
    /// </summary>
    /// <param name="text">Текст для ввода в поле</param>
    public virtual void Send(string text) 
        => WrappedItem.SendKeys(text);
}
