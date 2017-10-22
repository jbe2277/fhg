using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Waf.UnitTesting;
using Waf.FileHashGenerator.Presentation.Converters;
using Waf.FileHashGenerator.Presentation.Properties;

namespace Test.FileHashGenerator.Presentation.Converters
{
    [TestClass]
    public class IsHashValidToTextConverterTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            var converter = new IsHashValidToTextConverter();
            Assert.AreEqual(Resources.HashNotCompared, converter.Convert(null, null, null, null));
            Assert.AreEqual(Resources.HashValid, converter.Convert(true, null, null, null));
            Assert.AreEqual(Resources.HashNotValid, converter.Convert(false, null, null, null));

            AssertHelper.ExpectedException<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
