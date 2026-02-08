namespace SeleniumTests.Pages;

/// <summary>
/// Статический класс, содержащий URL-адреса страниц для тестирования.
/// Централизует управление URL в одном месте, упрощая изменение адресов
/// при переключении между окружениями (dev, staging, production).
/// </summary>
public static class Urls
{
    /// <summary>
    /// Получает URL главной страницы сайта Habr.com.
    /// Используется как точка входа для навигации в тестах.
    /// </summary>
    public static readonly string Main = "https://habr.com";
}
