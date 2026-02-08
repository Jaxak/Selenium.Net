using OpenQA.Selenium;

namespace SeleniumTests.Controls;

public class TextLabel(IWebElement webElement) : ControlBase(webElement)
{
    public string Text => WrappedItem.Text;
}