using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestCore.Browser;

public class ChromeFactory(IBrowserOptionsGetter<ChromeOptions> optionsProvider) : IBrowserFactory
{
    public async Task<IWebDriver> CreateAsync()
    {
        var options = await optionsProvider.GetOptionsAsync();
        return new ChromeDriver(options);
    }
}