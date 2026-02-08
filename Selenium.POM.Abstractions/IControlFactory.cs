using System;

namespace Selenium.POM.Abstractions;

public interface IControlFactory<in TWrappedItem>
{
    TControl Create<TControl>(IWrapper<TWrappedItem> wrapper, string dataTestId) 
        where TControl : IWrapper<TWrappedItem>;
    
    TControl Create<TControl>(Func<TWrappedItem> getItem)
        where TControl : IWrapper<TWrappedItem>;    
    
    TControl Create<TControl>(TWrappedItem item)
        where TControl : IWrapper<TWrappedItem>;
}