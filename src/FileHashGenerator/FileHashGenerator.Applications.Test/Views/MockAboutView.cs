using System.ComponentModel.Composition;
using System.Waf.UnitTesting.Mocks;
using Waf.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications.Views
{
    [Export(typeof(IAboutView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MockAboutView : MockDialogView<MockAboutView>, IAboutView
    {
    }
}
