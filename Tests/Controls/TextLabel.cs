using OpenQA.Selenium;

namespace SeleniumTests.Controls;

/// <summary>
/// Контрол текстовой метки (Label), представляющий элемент с текстовым содержимым на странице.
/// Используется для чтения и проверки текста из различных элементов: заголовков, параграфов, меток и т.д.
/// </summary>
/// <param name="webElement">Обернутый Selenium WebElement, содержащий текст</param>
public class TextLabel(IWebElement webElement) : ControlBase(webElement)
{
    /// <summary>
    /// Получает текстовое содержимое элемента.
    /// Возвращает видимый текст элемента, включая текст всех вложенных элементов.
    /// </summary>
    public string Text => WrappedItem.Text;
}
