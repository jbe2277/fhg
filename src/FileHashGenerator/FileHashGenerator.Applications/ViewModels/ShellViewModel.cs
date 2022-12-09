using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Applications.ViewModels;

public class ShellViewModel : ViewModel<IShellView>
{
    private object contentView = null!;
    private HashMode hashMode;
    private HashFormat hashFormat;

    public ShellViewModel(IShellView view) : base(view)
    {
        SelectSha512Command = new DelegateCommand(() => HashMode = HashMode.Sha512);
        SelectSha256Command = new DelegateCommand(() => HashMode = HashMode.Sha256);
        SelectSha1Command = new DelegateCommand(() => HashMode = HashMode.Sha1);
        SelectMD5Command = new DelegateCommand(() => HashMode = HashMode.MD5);
        SelectHexFormatCommand = new DelegateCommand(() => HashFormat = HashFormat.Hexadecimal);
        SelectBase64FormatCommand = new DelegateCommand(() => HashFormat = HashFormat.Base64);
        ShowWebsiteCommand = new DelegateCommand(ShowWebsite);
    }

    public string Title => ApplicationInfo.ProductName;

    public ICommand SelectSha512Command { get; }

    public ICommand SelectSha256Command { get; }

    public ICommand SelectSha1Command { get; }

    public ICommand SelectMD5Command { get; }

    public ICommand SelectHexFormatCommand { get; }

    public ICommand SelectBase64FormatCommand { get; }

    public ICommand OpenCommand { get; set; } = DelegateCommand.DisabledCommand;

    public object ContentView { get => contentView; set => SetProperty(ref contentView, value); }

    public HashMode HashMode { get => hashMode; set => SetProperty(ref hashMode, value); }

    public HashFormat HashFormat { get => hashFormat; set => SetProperty(ref hashFormat, value); }

    public ICommand ShowWebsiteCommand { get; }

    public string ProductName => ApplicationInfo.ProductName;

    public string Version => ApplicationInfo.Version;

    public string OSVersion => Environment.OSVersion.ToString();

    public string NetVersion => Environment.Version.ToString();

    public Architecture ProcessArchitecture => RuntimeInformation.ProcessArchitecture;

    public void Show() => ViewCore.Activate();

    private static void ShowWebsite(object? parameter)
    {
        string url = (string)parameter!;
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception e)
        {
            Trace.TraceError("An exception occured when trying to show the url '{0}'. Exception: {1}", url, e);
        }
    }
}
