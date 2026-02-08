using OpenQA.Selenium;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Controls;

public class Header(IWebElement webElement, IControlFactory controlFactory) : ControlBase(webElement)
{
    public Button Menu
        => controlFactory.Create<Button>(() => WrappedItem.FindElement(By.CssSelector("[aria-label='Toggle menu']")));
}