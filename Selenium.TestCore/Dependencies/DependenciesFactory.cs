using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SeleniumTestCore.Dependencies;

/// <summary>
/// Фабрика для создания зависимостей контролов и страниц Page Object Model.
/// Автоматически разрешает зависимости через контейнер DI, исключая стандартные типы Selenium,
/// которые передаются явно при создании объектов.
/// </summary>
public class DependenciesFactory(
    IServiceProvider serviceProvider,
    IDependenciesFilter filter)
    : IDependenciesFactory
{
    /// <summary>
    /// Создает массив зависимостей для указанного типа контрола или страницы.
    /// Анализирует единственный конструктор типа, фильтрует параметры и разрешает их через DI контейнер.
    /// </summary>
    /// <param name="controlType">Тип контрола или страницы, для которого создаются зависимости</param>
    /// <returns>Массив объектов-зависимостей, готовых для передачи в конструктор</returns>
    /// <exception cref="NotSupportedException">Выбрасывается, если тип имеет более одного конструктора</exception>
    public object[] CreateDependency(Type controlType)
    {
        var constructors = controlType.GetConstructors();
        if (constructors.Length != 1)
        {
            throw new NotSupportedException($"{controlType} должен иметь только один конструктор");
        }

        var constructor = constructors.Single();

        var parameters =
            filter
                .Apply(constructor.GetParameters())
                .Select<ParameterInfo, object>(x => serviceProvider.GetRequiredService(x.ParameterType));
        return parameters.ToArray();
    }
}
