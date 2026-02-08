namespace Selenium.POM.Abstractions;

public interface IWrapper<out TItem>
{
    TItem WrappedItem { get; }
}