using System.Threading.Tasks;

namespace SeleniumTestCore.Page;

/// <summary>
/// Интерфейс для страниц, требующих явного ожидания загрузки.
/// Позволяет страницам определить собственную логику ожидания полной загрузки всех элементов.
/// </summary>
public interface ILoadable
{
    /// <summary>
    /// Асинхронно ожидает полной загрузки страницы и всех её ключевых элементов.
    /// </summary>
    /// <returns>Задача, завершающаяся после полной загрузки страницы</returns>
    Task WaitLoadAsync();
}
