using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Presentation.DesignData
{
    public class SampleFileHashListViewModel : FileHashListViewModel
    {
        public SampleFileHashListViewModel() : base(new MockFileHashListView())
        {
            FileHashItems = new[] 
            {
                new FileHashItem(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Core.dll")
            };
            HashHeader = "SHA1";
        }



        private class MockFileHashListView : MockView, IFileHashListView
        {
        }
    }
}
