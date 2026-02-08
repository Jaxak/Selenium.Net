using System.Threading.Tasks;

namespace SeleniumTestCore.Page;

public interface ILoadable
{
    Task WaitLoadAsync();
}