using System.Waf.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications.ViewModels;

namespace Test.FileHashGenerator.Applications.ViewModels;

[TestClass]
public class FileHashListViewModelTest : TestClassBase
{
    [TestMethod]
    public void PropertiesTest()
    {
        var viewModel = Get<FileHashListViewModel>();

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashHeader, () => viewModel.HashHeader = "SHA1");
        Assert.AreEqual("SHA1", viewModel.HashHeader);
    }
}
