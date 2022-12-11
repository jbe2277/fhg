using Microsoft.UI.Xaml;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Applications.Views;
using Waf.FileHashGenerator.Presentation.Properties;

namespace Waf.FileHashGenerator.Presentation.Views;

public sealed partial class ShellWindow : Window, IShellView
{
    public ShellWindow()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel => (ShellViewModel)DataContext!;

    public object? DataContext { get; set; }

    public bool IsMode(HashMode actual, HashMode expected) => actual == expected;

    public string GetFormatText(HashFormat format) => format == HashFormat.Hexadecimal ? Resources.Hexadecimal : Resources.Base64;

    public string GetDotNetInfo() => $"{ViewModel.NetVersion} ({ViewModel.ProcessArchitecture})";
}
