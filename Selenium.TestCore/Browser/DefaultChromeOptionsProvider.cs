using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestCore.Browser;

/// <summary>
/// Провайдер настроек Chrome браузера по умолчанию.
/// Предоставляет стандартный набор опций для стабильной работы Chrome в режиме автоматизации,
/// включая поддержку headless режима для CI/CD окружения GitLab.
/// </summary>
public class DefaultChromeOptionsProvider : IBrowserOptionsGetter<ChromeOptions>
{
    private static bool IsGitLabCi 
        => Environment.GetEnvironmentVariable("GitLabCI") == "true";
    
    /// <summary>
    /// Асинхронно получает настроенные опции для Chrome браузера.
    /// Включает стандартные аргументы для стабильной работы в режиме автоматизации:
    /// - Запуск в headless режиме для GitLab CI
    /// - Разворот окна на весь экран
    /// - Игнорирование SSL ошибок
    /// - Отключение расширений и всплывающих окон
    /// - Настройки для работы в Docker/Linux окружении
    /// - Отключение детекции автоматизации
    /// </summary>
    /// <returns>Задача, возвращающая настроенный объект ChromeOptions</returns>
    public Task<ChromeOptions> GetOptionsAsync()
    { 
        var options = new ChromeOptions();
        if (IsGitLabCi)
        {
            options.AddArgument("--headless=new");
        }
        
        // Стандартные аргументы для стабильной работы
        options.AddArgument("--start-maximized"); // Окно на весь экран
        options.AddArgument("--ignore-certificate-errors"); // Игнорировать ошибки SSL
        options.AddArgument("--disable-extensions"); // Отключить расширения
        options.AddArgument("--disable-popup-blocking"); // Отключить блокировку всплывающих окон
        options.AddArgument("--disable-notifications"); // Отключить уведомления
        options.AddArgument("--no-sandbox"); // Важно для Docker/Linux
        options.AddArgument("--disable-dev-shm-usage"); // Предотвращает вылеты из-за нехватки памяти

        // Дополнительно: Отключение автоматизации (чтобы сайты реже понимали, что зашел бот)
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalOption("useAutomationExtension", false);
        options.PageLoadStrategy = PageLoadStrategy.Eager;
        return Task.FromResult(options);
    }
}
