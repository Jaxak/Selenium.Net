using System.Threading.Tasks;

namespace SeleniumTestCore.Browser;

public interface IBrowserOptionsGetter<TOptions>
{
    Task<TOptions> GetOptionsAsync();
}