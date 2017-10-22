﻿using System;
using System.Linq;
using System.Waf.Applications.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.FileHashGenerator.Applications.Services;
using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Controllers;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.ViewModels;
using System.Waf.UnitTesting.Mocks;

namespace Test.FileHashGenerator.Applications.Controllers
{
    [TestClass]
    public class ModuleControllerTest : TestClassBase
    {
        [TestMethod]
        public void ControllerLifecycle()
        {
            var controller = Container.GetExportedValue<ModuleController>();
            controller.Initialize();
            controller.Run();

            var shellView = Container.GetExportedValue<MockShellView>();
            var shellViewModel = Container.GetExportedValue<ShellViewModel>();

            // Show the about view
            bool aboutViewIsVisible = false;
            MockAboutView.ShowDialogAction = view =>
            {
                aboutViewIsVisible = true;
            };
            shellViewModel.AboutCommand.Execute(null);
            Assert.IsTrue(aboutViewIsVisible);

            // Close the application
            shellViewModel.Close();
            Assert.IsFalse(shellView.IsVisible);
            controller.Shutdown();
        }

        [TestMethod]
        public void OpenEmptyFile()
        {
            var controller = Container.GetExportedValue<ModuleController>();
            controller.Initialize();
            controller.Run();

            // Open file via OpenFileDialog
            var fileDialogService = Container.GetExportedValue<MockFileDialogService>();
            fileDialogService.Result = new FileDialogResult(@"Files\EmptyFile.txt", new FileType(Resources.AllFiles, ".*"));

            var shellViewModel = Container.GetExportedValue<ShellViewModel>();
            shellViewModel.OpenCommand.Execute(null);

            var fileHashListViewModel = Container.GetExportedValue<FileHashListViewModel>();
            var fileHashItem = fileHashListViewModel.FileHashItems.Single();

            shellViewModel.HashMode = HashMode.Sha512;

            // Wait some time so that the hash value is generated by another thread.
            DispatcherHelper.WaitUntil(() => fileHashItem.Hash != null, TimeSpan.FromSeconds(5));
            Assert.AreEqual("CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E", fileHashItem.Hash);

            shellViewModel.HashMode = HashMode.Sha256;
            
            DispatcherHelper.WaitUntil(() => fileHashItem.Hash != null, TimeSpan.FromSeconds(5));
            Assert.AreEqual("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855", fileHashItem.Hash);

            shellViewModel.HashMode = HashMode.Sha1;
            
            DispatcherHelper.WaitUntil(() => fileHashItem.Hash != null, TimeSpan.FromSeconds(5));
            Assert.AreEqual("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709", fileHashItem.Hash);

            shellViewModel.HashMode = HashMode.MD5;

            DispatcherHelper.WaitUntil(() => fileHashItem.Hash != null, TimeSpan.FromSeconds(5));
            Assert.AreEqual("D41D8CD98F00B204E9800998ECF8427E", fileHashItem.Hash);

            // Change formatting option
            shellViewModel.IsBase64Formatting = true;
            Assert.AreEqual("1B2M2Y8AsgTpgAmY7PhCfg==", fileHashItem.Hash);

            // Open the same file again
            shellViewModel.OpenCommand.Execute(null);
            Assert.AreEqual(fileHashItem, fileHashListViewModel.FileHashItems.Single());

            // Close the item
            fileHashListViewModel.CloseCommand.Execute(fileHashItem);
            Assert.IsFalse(fileHashListViewModel.FileHashItems.Any());

            controller.Shutdown();
        }

        [TestMethod]
        public void OpenMultipleFiles()
        {
            var controller = Container.GetExportedValue<ModuleController>();
            controller.Initialize();
            controller.Run();

            // Open files via drag and drop.
            var fileHashListViewModel = Container.GetExportedValue<FileHashListViewModel>();
            fileHashListViewModel.OpenFilesAction(new[] { @"Files\EmptyFile.txt", @"Files\ReferenceFile.txt" });
            Assert.AreEqual(2, fileHashListViewModel.FileHashItems.Count());
            var emptyFileHash = fileHashListViewModel.FileHashItems.First();
            var referenceFileHash = fileHashListViewModel.FileHashItems.Last();

            // Wait some time so that the hash value is generated by another thread.
            DispatcherHelper.WaitUntil(() => emptyFileHash.Hash != null, TimeSpan.FromSeconds(5));
            
            Assert.AreEqual(@"Files\EmptyFile.txt", emptyFileHash.FileName);
            Assert.AreEqual("DA39A3EE5E6B4B0D3255BFEF95601890AFD80709", emptyFileHash.Hash);

            Assert.AreEqual(@"Files\ReferenceFile.txt", referenceFileHash.FileName);
            Assert.AreEqual("2FD4E1C67A2D28FCED849EE1BB76E7391B93EB12", referenceFileHash.Hash);

            controller.Shutdown();
        }

        [TestMethod]
        public void OpenNotExistingFiles()
        {
            var controller = Container.GetExportedValue<ModuleController>();
            controller.Initialize();

            var messageService = Container.GetExportedValue<MockMessageService>();
            var fileHashListViewModel = Container.GetExportedValue<FileHashListViewModel>();
            var environmentService = Container.GetExportedValue<MockEnvironmentService>();
            
            // Open files via command line parameters
            environmentService.DocumentFileNames = new[] { "NotExistingFile1", "NotExistingFile2" };
            messageService.Clear();
            controller.Run();

            Assert.AreEqual(MessageType.Error, messageService.MessageType);
            Assert.IsNotNull(messageService.Message);
            Assert.IsNotNull(messageService.Owner);

            Assert.IsFalse(fileHashListViewModel.FileHashItems.Any());
            
            controller.Shutdown();
        }
    }
}