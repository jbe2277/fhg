using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.UnitTesting;
using Waf.FileHashGenerator.Applications;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications;

[TestClass]
public abstract class TestClassBase
{
    private IServiceProvider serviceProvider = null!;

    protected UnitTestSynchronizationContext Context { get; private set; } = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        Context = UnitTestSynchronizationContext.Create();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplications().AddApplicationsTest().AddSingletonLazySupport();
        serviceProvider = serviceCollection.BuildServiceProvider();
        OnTestInitialize();
    }

    [TestCleanup]
    public void TestCleanup() => OnTestCleanup();
    
    public T Get<T>() where T : class => serviceProvider.GetRequiredService<T>();

    protected virtual void OnTestInitialize() { }

    protected virtual void OnTestCleanup() { }
}