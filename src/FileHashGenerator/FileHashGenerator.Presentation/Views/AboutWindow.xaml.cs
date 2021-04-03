using System.Windows;
using System.ComponentModel.Composition;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.Views
{
    [Export(typeof(IAboutView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AboutWindow : IAboutView
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            ShowDialog();
        }
    }
}
