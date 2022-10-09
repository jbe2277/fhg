using System.Waf.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Domain;

namespace Test.FileHashGenerator.Domain;

[TestClass]
public class FileHashItemTest
{
    [TestMethod]
    public void IsHashValidTest()
    {
        var item = new FileHashItem(@"c:\test.txt");

        Assert.AreEqual(@"c:\test.txt", item.FileName);

        AssertHelper.PropertyChangedEvent(item, x => x.Progress, () => item.Progress = 0.5);
        Assert.AreEqual(0.5, item.Progress);

        Assert.IsNull(item.IsHashValid);

        AssertHelper.PropertyChangedEvent(item, x => x.Hash, () => item.Hash = "1234");
        Assert.AreEqual("1234", item.Hash);

        Assert.IsNull(item.IsHashValid);

        AssertHelper.PropertyChangedEvent(item, x => x.ExpectedHash, () => item.ExpectedHash = "123");
        Assert.AreEqual("123", item.ExpectedHash);

        Assert.IsTrue(item.IsHashValid == false);

        AssertHelper.PropertyChangedEvent(item, x => x.IsHashValid, () => item.ExpectedHash = "1234");
        Assert.IsTrue(item.IsHashValid == true);

        AssertHelper.PropertyChangedEvent(item, x => x.IsHashValid, () => item.Hash = null);
        Assert.IsNull(item.IsHashValid);
    }

    [TestMethod]
    public void CaseSensitiveValidTest()
    {
        var item = new FileHashItem(@"c:\test.txt");

        item.IsCaseSensitive = false;
        item.Hash = "D41D8CD98F00B204E9800998ECF8427E";
        item.ExpectedHash = "D41D8CD98F00B204E9800998ECF8427E";
        Assert.IsTrue(item.IsHashValid == true);

        item.ExpectedHash = "D41D8CD98F00B204E9800998ECF8427E".ToLowerInvariant();
        Assert.IsTrue(item.IsHashValid == true);

        item.IsCaseSensitive = true;
        item.Hash = "1B2M2Y8AsgTpgAmY7PhCfg==";
        item.ExpectedHash = "1B2M2Y8AsgTpgAmY7PhCfg==";
        Assert.IsTrue(item.IsHashValid == true);

        item.ExpectedHash = "1B2M2Y8AsgTpgAmY7PhCfg==".ToLowerInvariant();
        Assert.IsTrue(item.IsHashValid == false);
    }
}
