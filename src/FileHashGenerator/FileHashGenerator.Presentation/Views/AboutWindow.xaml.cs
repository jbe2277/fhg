using System.Windows;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Presentation.Views;

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
