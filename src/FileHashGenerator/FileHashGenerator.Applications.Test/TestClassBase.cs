using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Waf.UnitTesting;
using System.Waf.UnitTesting.Mocks;
using Waf.FileHashGenerator.Applications.ViewModels;

namespace Test.FileHashGenerator.Applications
{
    [TestClass]
    public abstract class TestClassBase
    {
        private AggregateCatalog catalog = null!;

        protected UnitTestSynchronizationContext Context { get; private set; } = null!;

        protected CompositionContainer Container { get; private set; } = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            Context = UnitTestSynchronizationContext.Create();

            catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ShellViewModel).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MockMessageService).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TestClassBase).Assembly));

            Container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(Container);
            Container.Compose(batch);

            OnTestInitialize();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            OnTestCleanup();

            Container.Dispose();
            catalog.Dispose();
            Context.Dispose();
        }

        protected virtual void OnTestInitialize() { }

        protected virtual void OnTestCleanup() { }
    }
}