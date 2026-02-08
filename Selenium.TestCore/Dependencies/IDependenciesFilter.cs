using System.Collections.Generic;
using System.Reflection;

namespace SeleniumTestCore.Dependencies;

/// <summary>
/// Интерфейс фильтра зависимостей для исключения определенных типов параметров из автоматического разрешения.
/// Используется для фильтрации параметров конструктора перед их разрешением через DI контейнер.
/// </summary>
public interface IDependenciesFilter
{
    /// <summary>
    /// Применяет фильтр к коллекции параметров конструктора.
    /// </summary>
    /// <param name="getParameters">Коллекция параметров для фильтрации</param>
    /// <returns>Отфильтрованная коллекция параметров</returns>
    IEnumerable<ParameterInfo> Apply(IEnumerable<ParameterInfo> getParameters);
}
