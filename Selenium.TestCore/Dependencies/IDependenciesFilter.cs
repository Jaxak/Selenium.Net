using System.Collections.Generic;
using System.Reflection;

namespace SeleniumTestCore.Dependencies;

public interface IDependenciesFilter
{
    IEnumerable<ParameterInfo> Apply(IEnumerable<ParameterInfo> getParameters);
}