using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.DesignData;

public class SampleAboutViewModel : AboutViewModel
{
    public SampleAboutViewModel() : base(new MockAboutView())
    {
    }

    public new string ProductName => "DesignTime File Hash Generator";

    public new string Version => "1.0.0.0";


    private class MockAboutView : MockView, IAboutView
    {
        public void ShowDialog(object owner) { }
    }
}
