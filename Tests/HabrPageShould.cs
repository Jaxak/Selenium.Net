using NUnit.Framework;
using SeleniumTests.Infra;
using SeleniumTests.Pages;

namespace SeleniumTests;

public class HabrPageShould : TestBase
{
    [TestCase("Что нового")]
    public async Task ContainsTitle(string substring)
    {
        var page = await Navigation.GoToPageAsync<HabrPage>(Urls.Main);
        page.Header.Menu.Click();
        Assert.That(
            page.MenuSidePage.NewsButton.WrappedItem.Text,
            Contains.Substring(substring).After(5000, 100)
        );
    }
}