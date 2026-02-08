using OpenQA.Selenium;
using SeleniumTestCore.Controls;

namespace SeleniumTests.Controls;

public class MenuSidePage(IWebElement webElement, IControlFactory controlFactory) : ControlBase(webElement)
{
    public Button NewsButton
        => controlFactory.Create<Button>(this, "whats-new-menu-item");
}