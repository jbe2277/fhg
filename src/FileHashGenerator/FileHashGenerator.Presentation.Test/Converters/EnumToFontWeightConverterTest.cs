using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Waf.UnitTesting;
using System.Windows;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Presentation.Converters;

namespace Test.FileHashGenerator.Presentation.Converters
{
    [TestClass]
    public class EnumToFontWeightConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new EnumToFontWeightConverter();
            Assert.AreEqual(DependencyProperty.UnsetValue, converter.Convert(HashMode.Sha512, null, "Sha1", null));
            Assert.AreEqual(FontWeights.SemiBold, converter.Convert(HashMode.Sha512, null, "Sha512", null));

            AssertHelper.ExpectedException<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
