﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.ViewModels;
using System.Waf.Applications;
using System.Waf.UnitTesting;
using System.Waf.Applications.Services;

namespace Test.FileHashGenerator.Applications.ViewModels
{
    [TestClass]
    public class ShellViewModelTest : TestClassBase
    {
        [TestMethod]
        public void ShowAndClose()
        {
            var shellView = Container.GetExportedValue<MockShellView>();
            var shellViewModel = Container.GetExportedValue<ShellViewModel>();

            // Show the ShellView
            Assert.IsFalse(shellView.IsVisible);
            shellViewModel.Show();
            Assert.IsTrue(shellView.IsVisible);

            // Close the ShellView via the ExitCommand
            shellViewModel.Close();
            Assert.IsFalse(shellView.IsVisible);
        }

        [TestMethod]
        public void RestoreWindowLocationAndSize()
        {
            var shellView = Container.GetExportedValue<MockShellView>();
            shellView.VirtualScreenWidth = 1000;
            shellView.VirtualScreenHeight = 700;

            var settingsService = Container.GetExportedValue<ISettingsService>();
            var settings = settingsService.Get<AppSettings>();
            SetSettingsValues(settings, 20, 10, 400, 300, true);

            var shellViewModel = Container.GetExportedValue<ShellViewModel>();
            Assert.AreEqual(20, shellView.Left);
            Assert.AreEqual(10, shellView.Top);
            Assert.AreEqual(400, shellView.Width);
            Assert.AreEqual(300, shellView.Height);
            Assert.IsTrue(shellView.IsMaximized);

            shellView.Left = 25;
            shellView.Top = 15;
            shellView.Width = 450;
            shellView.Height = 350;
            shellView.IsMaximized = false;

            shellView.Close();
            AssertSettingsValues(settings, 25, 15, 450, 350, false);
        }

        [TestMethod]
        public void RestoreWindowLocationAndSizeSpecial()
        {
            var shellView = Container.GetExportedValue<MockShellView>();
            shellView.VirtualScreenWidth = 1000;
            shellView.VirtualScreenHeight = 700;

            var settingsService = Container.GetExportedValue<ISettingsService>();
            var settings = settingsService.Get<AppSettings>();
            shellView.SetNAForLocationAndSize();

            SetSettingsValues(settings);
            new ShellViewModel(shellView, settingsService).Close();
            AssertSettingsValues(settings, double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Height is 0 => don't apply the Settings values
            SetSettingsValues(settings, 0, 0, 1, 0);
            new ShellViewModel(shellView, settingsService).Close();
            AssertSettingsValues(settings, double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Left = 100 + Width = 901 > VirtualScreenWidth = 1000 => don't apply the Settings values
            SetSettingsValues(settings, 100, 100, 901, 100);
            new ShellViewModel(shellView, settingsService).Close();
            AssertSettingsValues(settings, double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Top = 100 + Height = 601 > VirtualScreenWidth = 600 => don't apply the Settings values
            SetSettingsValues(settings, 100, 100, 100, 601);
            new ShellViewModel(shellView, settingsService).Close();
            AssertSettingsValues(settings, double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Use the limit values => apply the Settings values
            SetSettingsValues(settings, 0, 0, 1000, 700);
            new ShellViewModel(shellView, settingsService).Close();
            AssertSettingsValues(settings, 0, 0, 1000, 700, false);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            var viewModel = Container.GetExportedValue<ShellViewModel>();

            var openCommand = new DelegateCommand(() => { });
            AssertHelper.PropertyChangedEvent(viewModel, x => x.OpenCommand, () => viewModel.OpenCommand = openCommand);
            Assert.AreEqual(openCommand, viewModel.OpenCommand);

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.HashMode = HashMode.MD5);
            Assert.AreEqual(HashMode.MD5, viewModel.HashMode);

            viewModel.IsHexadecimalFormatting = true;
            AssertHelper.PropertyChangedEvent(viewModel, x => x.IsHexadecimalFormatting, () => viewModel.IsBase64Formatting = true);
            Assert.IsFalse(viewModel.IsHexadecimalFormatting);
            Assert.IsTrue(viewModel.IsBase64Formatting);

            AssertHelper.PropertyChangedEvent(viewModel, x => x.IsBase64Formatting, () => viewModel.IsHexadecimalFormatting = true);
            Assert.IsTrue(viewModel.IsHexadecimalFormatting);
            Assert.IsFalse(viewModel.IsBase64Formatting);

            var aboutCommand = new DelegateCommand(() => { });
            AssertHelper.PropertyChangedEvent(viewModel, x => x.AboutCommand, () => viewModel.AboutCommand = aboutCommand);
            Assert.AreEqual(aboutCommand, viewModel.AboutCommand);

            object contentView = new object();
            AssertHelper.PropertyChangedEvent(viewModel, x => x.ContentView, () => viewModel.ContentView = contentView);
            Assert.AreEqual(contentView, viewModel.ContentView);
        }

        [TestMethod]
        public void SelectHashModeTest()
        {
            var viewModel = Container.GetExportedValue<ShellViewModel>();

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectMD5Command.Execute(null));
            Assert.AreEqual(HashMode.MD5, viewModel.HashMode);

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha1Command.Execute(null));
            Assert.AreEqual(HashMode.Sha1, viewModel.HashMode);

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha256Command.Execute(null));
            Assert.AreEqual(HashMode.Sha256, viewModel.HashMode);

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HashMode, () => viewModel.SelectSha512Command.Execute(null));
            Assert.AreEqual(HashMode.Sha512, viewModel.HashMode);
        }

        private static void SetSettingsValues(AppSettings settings, double left = 0, double top = 0, double width = 0, double height = 0, bool isMaximized = false)
        {
            settings.Left = left;
            settings.Top = top;
            settings.Width = width;
            settings.Height = height;
            settings.IsMaximized = isMaximized;
        }

        private static void AssertSettingsValues(AppSettings settings, double left, double top, double width, double height, bool isMaximized)
        {
            Assert.AreEqual(left, settings.Left);
            Assert.AreEqual(top, settings.Top);
            Assert.AreEqual(width, settings.Width);
            Assert.AreEqual(height, settings.Height);
            Assert.AreEqual(isMaximized, settings.IsMaximized);
        }
    }
}
