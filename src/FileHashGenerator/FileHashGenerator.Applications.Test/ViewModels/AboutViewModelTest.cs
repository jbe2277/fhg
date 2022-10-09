using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications.ViewModels;
using Test.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications.ViewModels;

[TestClass]
public class AboutViewModelTest : TestClassBase
{
    [TestMethod]
    public void PropertiesTest()
    {
        var aboutView = new MockAboutView();
        var aboutViewModel = new AboutViewModel(aboutView);
        var productName = aboutViewModel.ProductName;
        var version = aboutViewModel.Version;
        var osVersion = aboutViewModel.OSVersion;
        var netVersion = aboutViewModel.NetVersion;

        Assert.IsFalse(string.IsNullOrEmpty(osVersion));
        Assert.IsFalse(string.IsNullOrEmpty(netVersion));

        Assert.IsNotNull(aboutViewModel.ShowWebsiteCommand);
    }
}
