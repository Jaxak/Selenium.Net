using NUnit.Framework;
using SeleniumTests.Infra;
using SeleniumTests.Pages;

namespace SeleniumTests;

/// <summary>
/// Набор тестов для проверки функциональности главной страницы Habr.com.
/// Наследуется от TestBase для получения доступа к настроенным сервисам и навигации.
/// Все тесты выполняются параллельно благодаря атрибуту Parallelizable в базовом классе.
/// </summary>
public class HabrPageShould : TestBase
{
    /// <summary>
    /// Проверяет, что боковое меню содержит указанный текст после открытия.
    /// Тест выполняет следующие шаги:
    /// 1. Открывает главную страницу Habr.com
    /// 2. Кликает на кнопку меню в шапке
    /// 3. Проверяет, что кнопка "Новости" в боковом меню содержит ожидаемый текст
    /// </summary>
    /// <param name="substring">Подстрока, которая должна содержаться в тексте кнопки</param>
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
