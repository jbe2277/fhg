using System.ComponentModel.Composition;
using System.Waf.UnitTesting.Mocks;
using Waf.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications.Views;

[Export, Export(typeof(IFileHashListView))]
public class MockFileHashListView : MockView, IFileHashListView
{
}
