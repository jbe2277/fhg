using System;
using System.IO;
using System.Threading;
using System.Waf.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.FileHashGenerator.Applications;

namespace Test.FileHashGenerator.Applications
{
    [TestClass]
    public class ProgressStreamTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            AssertHelper.ExpectedException<ArgumentNullException>(() => new ProgressStream(null, new CancellationToken(), new Progress<double>()));
            AssertHelper.ExpectedException<ArgumentNullException>(() => new ProgressStream(new MemoryStream(), new CancellationToken(), null));
        }

        [TestMethod]
        public void BasicStreamTest()
        {
            using (var stream = new ProgressStream(new MemoryStream(), new CancellationToken(), new Progress<double>()))
            {
                Assert.IsTrue(stream.CanRead);
                Assert.IsTrue(stream.CanSeek);
                Assert.IsTrue(stream.CanWrite);
                Assert.AreEqual(0, stream.Length);
                Assert.AreEqual(0, stream.Position);

                stream.Flush();
            }
        }
    }
}
