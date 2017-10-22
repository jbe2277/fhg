using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications.ViewModels;
using Test.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications.ViewModels
{
    [TestClass]
    public class AboutViewModelTest : TestClassBase
    {
        [TestMethod]
        public void PropertiesTest()
        {
            MockAboutView aboutView = new MockAboutView();
            AboutViewModel aboutViewModel = new AboutViewModel(aboutView);
            var productName = aboutViewModel.ProductName;
            var version = aboutViewModel.Version;
            var osVersion = aboutViewModel.OSVersion;
            var netVersion = aboutViewModel.NetVersion;
            var is64BitProcess = aboutViewModel.Is64BitProcess;

            Assert.IsFalse(string.IsNullOrEmpty(osVersion));
            Assert.IsFalse(string.IsNullOrEmpty(netVersion));

            Assert.IsNotNull(aboutViewModel.ShowWebsiteCommand);
        }
    }
}
