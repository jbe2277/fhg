using System.Waf.Applications;
using System.Waf.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Domain;

namespace Test.FileHashGenerator.Applications.ViewModels;

[TestClass]
public class FileHashListViewModelTest : TestClassBase
{
    [TestMethod]
    public void PropertiesTest()
    {
        var viewModel = Get<FileHashListViewModel>();

        var fileHashItems = new List<FileHashItem>();
        AssertHelper.PropertyChangedEvent(viewModel, x => x.FileHashItems, () => viewModel.FileHashItems = fileHashItems);
        Assert.AreEqual(fileHashItems, viewModel.FileHashItems);

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashHeader, () => viewModel.HashHeader = "SHA1");
        Assert.AreEqual("SHA1", viewModel.HashHeader);

        var closeCommand = new DelegateCommand(() => { });
        AssertHelper.PropertyChangedEvent(viewModel, x => x.CloseCommand, () => viewModel.CloseCommand = closeCommand);
        Assert.AreEqual(closeCommand, viewModel.CloseCommand);
    }
}
