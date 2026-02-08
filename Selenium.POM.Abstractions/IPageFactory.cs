using System;

namespace Selenium.POM.Abstractions;

public interface IPageFactory<in TWrappedItem>
{
    TPage Create<TPage>(TWrappedItem page)
        where TPage : IWrapper<TWrappedItem>;
    
    TPage Create<TPage>(Func<TWrappedItem> getPage)
        where TPage : IWrapper<TWrappedItem>;
}