using NUnit.Framework;
using SeleniumTests.Infra;

namespace SeleniumTests;

[SetUpFixture]
public class GlobalDispose
{
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
        => await ServiceProviderCache.GlobalDisposeAsync();
}