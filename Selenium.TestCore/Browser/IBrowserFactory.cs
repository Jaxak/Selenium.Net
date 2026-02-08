using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

public interface IBrowserFactory
{
    Task<IWebDriver> CreateAsync();
}