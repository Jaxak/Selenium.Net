using System;

namespace SeleniumTestCore.Dependencies;

public interface IDependenciesFactory
{
    object[] CreateDependency(Type controlType);
}