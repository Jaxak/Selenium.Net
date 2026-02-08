using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

public interface IBrowserGetter
{
    Task<IWebDriver> GetAsync();
}