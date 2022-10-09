using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.UnitTesting;
using Waf.FileHashGenerator.Presentation.Converters;

namespace Test.FileHashGenerator.Presentation.Converters;

[TestClass]
public class FileNameConverterTest
{
    [TestMethod]
    public void ConvertTest()
    {
        var converter = new FileNameConverter();
        Assert.AreEqual(@"Test 3.txt", converter.Convert(@"c:\Directory 1\Subdirectory 2\Test 3.txt", null, null, null));

        AssertHelper.ExpectedException<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
    }
}
