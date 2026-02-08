using NUnit.Framework;
using SeleniumTests.Infra;

namespace SeleniumTests;

/// <summary>
/// Глобальный класс для освобождения ресурсов после выполнения всех тестов в сборке.
/// Используется NUnit атрибут SetUpFixture для выполнения глобальной очистки один раз после всех тестов.
/// Гарантирует корректное завершение работы всех браузеров и освобождение системных ресурсов.
/// </summary>
[SetUpFixture]
public class GlobalDispose
{
    /// <summary>
    /// Выполняется один раз после завершения всех тестов в сборке.
    /// Асинхронно освобождает все кэшированные ServiceProvider и связанные с ними ресурсы (браузеры, драйверы).
    /// </summary>
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
        => await ServiceProviderCache.GlobalDisposeAsync();
}
