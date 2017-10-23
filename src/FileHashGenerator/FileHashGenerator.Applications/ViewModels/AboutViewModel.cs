using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Applications.ViewModels
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class AboutViewModel : ViewModel<IAboutView>
    {
        [ImportingConstructor]
        public AboutViewModel(IAboutView view) : base(view)
        {
            ShowWebsiteCommand = new DelegateCommand(ShowWebsite);
        }


        public ICommand ShowWebsiteCommand { get; }

        public string ProductName => ApplicationInfo.ProductName;

        public string Version => ApplicationInfo.Version;

        public string OSVersion => Environment.OSVersion.ToString();

        public string NetVersion => Environment.Version.ToString();

        public bool Is64BitProcess => Environment.Is64BitProcess;


        public void ShowDialog(object owner)
        {
            ViewCore.ShowDialog(owner);
        }

        private void ShowWebsite(object parameter)
        {
            string url = (string)parameter;
            try
            {
                Process.Start(url);
            }
            catch (Exception e)
            {
                Trace.TraceError("An exception occured when trying to show the url '{0}'. Exception: {1}", url, e);
            }
        }
    }
}
