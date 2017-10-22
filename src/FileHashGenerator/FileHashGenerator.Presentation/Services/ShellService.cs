using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Shell;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Presentation.Views;

namespace Waf.FileHashGenerator.Presentation.Services
{
    [Export(typeof(IShellService))]
    public class ShellService : IShellService
    {
        private readonly Lazy<ShellWindow> shellView;
        private readonly Dictionary<object, double> progressReports;


        [ImportingConstructor]
        public ShellService(Lazy<ShellWindow> shellView)
        {
            this.shellView = shellView;
            this.progressReports = new Dictionary<object, double>();
        }
        

        public object ShellView { get { return shellView.Value; } }


        public void UpdateProgress(object source, double progress)
        {
            progressReports[source] = progress;
            UpdateTaskbarItem();
        }

        public void RemoveProgress(object source)
        {
            progressReports.Remove(source);
            UpdateTaskbarItem();
        }

        private void UpdateTaskbarItem()
        {
            if (progressReports.Any())
            {
                shellView.Value.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
                double value = progressReports.Values.Sum() / progressReports.Count;
                shellView.Value.TaskbarItemInfo.ProgressValue = value;
            }
            else
            {
                shellView.Value.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
            }
        }
    }
}
