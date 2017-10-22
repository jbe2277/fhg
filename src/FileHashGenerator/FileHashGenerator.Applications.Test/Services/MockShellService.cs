using System.Collections.Generic;
using System.ComponentModel.Composition;
using Test.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Applications.Services;

namespace Test.FileHashGenerator.Applications.Services
{
    [Export, Export(typeof(IShellService))]
    public class MockShellService : IShellService
    {
        [ImportingConstructor]
        public MockShellService(MockShellView shellView)
        {
            this.ShellView = shellView;
            this.ProgressReports = new Dictionary<object, double>();
        }
        

        public object ShellView { get; private set; }

        public Dictionary<object, double> ProgressReports { get; private set; }


        public void UpdateProgress(object source, double progress)
        {
            ProgressReports[source] = progress;
        }

        public void RemoveProgress(object source)
        {
            ProgressReports.Remove(source);
        }
    }
}
