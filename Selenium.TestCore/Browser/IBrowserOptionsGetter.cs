using System.Threading.Tasks;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Интерфейс провайдера опций для конфигурации браузера.
/// Позволяет динамически формировать настройки браузера в зависимости от окружения или требований.
/// </summary>
/// <typeparam name="TOptions">Тип опций браузера (например, ChromeOptions, FirefoxOptions)</typeparam>
public interface IBrowserOptionsGetter<TOptions>
{
    /// <summary>
    /// Асинхронно получает настроенные опции для браузера.
    /// </summary>
    /// <returns>Задача, возвращающая объект опций типа <typeparamref name="TOptions"/></returns>
    Task<TOptions> GetOptionsAsync();
}
