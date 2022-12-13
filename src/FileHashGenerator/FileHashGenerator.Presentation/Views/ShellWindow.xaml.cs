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
        // TODO: Support to change Theme by user: https://github.com/microsoft/WinUI-Gallery/blob/c93d37823fb333214b948d30451f13cc47b04abe/WinUIGallery/Helper/ThemeHelper.cs
        //if (Content is FrameworkElement element) element.RequestedTheme = ElementTheme.Dark;
    }

    public ShellViewModel ViewModel => (ShellViewModel)DataContext!;

    public object? DataContext { get; set; }

    public bool IsMode(HashMode actual, HashMode expected) => actual == expected;

    public string GetFormatText(HashFormat format) => format == HashFormat.Hexadecimal ? Resources.Hexadecimal : Resources.Base64;

    public string GetDotNetInfo() => $"{ViewModel.NetVersion} ({ViewModel.ProcessArchitecture})";
}
