using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Presentation.Properties;
using Waf.FileHashGenerator.Presentation.Views;

namespace Test.FileHashGenerator.Presentation.Views;

[TestClass]
public class ViewConvertTest
{
    [TestMethod]
    public void ConvertTest()
    {
        Assert.AreEqual("Name: Luke", ViewConvert.Format("Name: {0}", "Luke"));

        Assert.AreEqual(Visibility.Collapsed, ViewConvert.AnyToVisibility(0));
        Assert.AreEqual(Visibility.Visible, ViewConvert.AnyToVisibility(1));

        Assert.AreEqual(Visibility.Visible, ViewConvert.IsEmptyToVisibility(0));
        Assert.AreEqual(Visibility.Collapsed, ViewConvert.IsEmptyToVisibility(1));

        Assert.AreEqual(@"Test 3.txt", ViewConvert.GetFileName(@"c:\Directory 1\Subdirectory 2\Test 3.txt"));
        Assert.IsNull(ViewConvert.GetFileName(null));

        Assert.AreEqual(@"c:\Directory 1\Subdirectory 2", ViewConvert.GetDirectoryName(@"c:\Directory 1\Subdirectory 2\Test 3.txt"));
        Assert.IsNull(ViewConvert.GetDirectoryName(null));

        Assert.AreEqual(0, ViewConvert.NullIsOpacityZero(null));
        Assert.AreEqual(1, ViewConvert.NullIsOpacityZero("something"));

        Assert.AreEqual(Visibility.Visible, ViewConvert.NullIsVisible(null));
        Assert.AreEqual(Visibility.Collapsed, ViewConvert.NullIsVisible("something"));

        Assert.AreEqual(InfoBarSeverity.Success, ViewConvert.BoolToSeverity(true));
        Assert.AreEqual(InfoBarSeverity.Error, ViewConvert.BoolToSeverity(false));
        Assert.AreEqual(InfoBarSeverity.Informational, ViewConvert.BoolToSeverity(null));

        Assert.AreEqual(Resources.HashValid, ViewConvert.IsHashValidToText(true));
        Assert.AreEqual(Resources.HashNotValid, ViewConvert.IsHashValidToText(false));
        Assert.AreEqual(Resources.HashNotCompared, ViewConvert.IsHashValidToText(null));
    }
}
