using OpenQA.Selenium;

namespace SeleniumTests.Controls;

public class Input(IWebElement webElement) : ControlBase(webElement)
{
    public virtual void Send(string text) 
        => WrappedItem.SendKeys(text);
}