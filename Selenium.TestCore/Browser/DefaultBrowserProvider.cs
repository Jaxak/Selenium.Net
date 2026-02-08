using System;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTestCore.Browser;

public class DefaultBrowserProvider(IBrowserFactory browserFactory) 
    : IBrowserGetter, IAsyncDisposable, IDisposable
{
    private readonly Lazy<Task<IWebDriver>> _driver = new(browserFactory.CreateAsync);

    public Task<IWebDriver> GetAsync()
        => _driver.Value;

    public async ValueTask DisposeAsync()
    {
        if (_driver.IsValueCreated)
        {
            var driver = await _driver.Value;
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }

    public void Dispose()
        => DisposeAsync().GetAwaiter().GetResult();
}