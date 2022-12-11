using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications.ViewModels;
using System.Waf.Applications;
using System.Waf.UnitTesting;
using System.Waf.Applications.Services;

namespace Test.FileHashGenerator.Applications.ViewModels;

[TestClass]
public class ShellViewModelTest : TestClassBase
{
    [TestMethod]
    public void PropertiesTest()
    {
        var viewModel = Get<ShellViewModel>();

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.HashMode = HashMode.MD5);
        Assert.AreEqual(HashMode.MD5, viewModel.HashMode);

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashFormat, () => viewModel.HashFormat = HashFormat.Base64);
        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashFormat, () => viewModel.HashFormat = HashFormat.Hexadecimal);

        var contentView = new object();
        AssertHelper.PropertyChangedEvent(viewModel, x => x.ContentView, () => viewModel.ContentView = contentView);
        Assert.AreEqual(contentView, viewModel.ContentView);
    }

    [TestMethod]
    public void SelectHashModeTest()
    {
        var viewModel = Get<ShellViewModel>();

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectMD5Command.Execute(null));
        Assert.AreEqual(HashMode.MD5, viewModel.HashMode);

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha1Command.Execute(null));
        Assert.AreEqual(HashMode.Sha1, viewModel.HashMode);

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha256Command.Execute(null));
        Assert.AreEqual(HashMode.Sha256, viewModel.HashMode);

        AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha512Command.Execute(null));
        Assert.AreEqual(HashMode.Sha512, viewModel.HashMode);
    }
}
