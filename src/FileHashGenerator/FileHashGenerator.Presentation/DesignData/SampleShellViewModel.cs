using System;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.DesignData
{
    public class SampleShellViewModel : ShellViewModel
    {
        public SampleShellViewModel() : base(new MockShellView(), new MockSettingsService())
        {

        }


        private class MockShellView : MockView, IShellView
        {
            public double VirtualScreenWidth => 0;
            
            public double VirtualScreenHeight => 0;
            
            public double Left { get; set; }
            
            public double Top { get; set; }
            
            public double Width { get; set; }
            
            public double Height { get; set; }
            
            public bool IsMaximized { get; set; }
            
            public event EventHandler Closed;

            public void Show()
            {
            }

            public void Close()
            {
            }

            protected virtual void OnClosed(EventArgs e)
            {
                Closed?.Invoke(this, e);
            }
        }
    }
}
