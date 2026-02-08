using System;

namespace SeleniumTestCore.Dependencies;

/// <summary>
/// Интерфейс фабрики для создания зависимостей контролов и страниц.
/// Определяет контракт для автоматического разрешения зависимостей через контейнер DI.
/// </summary>
public interface IDependenciesFactory
{
    /// <summary>
    /// Создает массив зависимостей для указанного типа.
    /// </summary>
    /// <param name="controlType">Тип, для которого создаются зависимости</param>
    /// <returns>Массив объектов-зависимостей</returns>
    object[] CreateDependency(Type controlType);
}
