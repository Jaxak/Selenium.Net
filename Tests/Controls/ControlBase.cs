using OpenQA.Selenium;
using Selenium.POM.Abstractions;

namespace SeleniumTests.Controls;

public abstract class ControlBase(IWebElement webElement) : IWrapper<IWebElement>
{
    public IWebElement WrappedItem { get; } = webElement;
    public virtual void Click() 
        => WrappedItem.Click();
    
    public bool IsVisible
        => WrappedItem.Displayed;
}