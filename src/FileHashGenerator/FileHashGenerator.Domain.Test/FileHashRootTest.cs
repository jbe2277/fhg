using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Domain;

namespace Test.FileHashGenerator.Domain
{
    [TestClass]
    public class FileHashRootTest
    {
        [TestMethod]
        public void AddAndRemoveFileHashItemTest()
        {
            var root = new FileHashRoot();
            Assert.IsFalse(root.FileHashItems.Any());

            var fileHashItem = root.AddNewFileHashItem(@"c:\test.txt");
            Assert.AreEqual(fileHashItem, root.FileHashItems.Single());
            Assert.AreEqual(@"c:\test.txt", fileHashItem.FileName);

            root.RemoveFileHashItem(fileHashItem);
            Assert.IsFalse(root.FileHashItems.Any());
        }
    }
}
