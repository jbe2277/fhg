using System.Waf.Applications;
using System.Waf.Applications.Services;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Applications.ViewModels;

public class ShellViewModel : ViewModel<IShellView>
{
    private readonly AppSettings settings;
    private ICommand openCommand = DelegateCommand.DisabledCommand;
    private ICommand aboutCommand = DelegateCommand.DisabledCommand;
    private object contentView = null!;
    private HashMode hashMode;
    private bool isHexadecimalFormatting;
    private bool isBase64Formatting;

    public ShellViewModel(IShellView view, ISettingsService settingsService) : base(view)
    {
        SelectSha512Command = new DelegateCommand(() => HashMode = HashMode.Sha512);
        SelectSha256Command = new DelegateCommand(() => HashMode = HashMode.Sha256);
        SelectSha1Command = new DelegateCommand(() => HashMode = HashMode.Sha1);
        SelectMD5Command = new DelegateCommand(() => HashMode = HashMode.MD5);
        isHexadecimalFormatting = true;
        settings = settingsService.Get<AppSettings>();
        view.Closed += ViewClosed;

        // Restore the window size when the values are valid.
        if (settings.Left >= 0 && settings.Top >= 0 && settings.Width > 0 && settings.Height > 0
            && settings.Left + settings.Width <= view.VirtualScreenWidth
            && settings.Top + settings.Height <= view.VirtualScreenHeight)
        {
            ViewCore.Left = settings.Left;
            ViewCore.Top = settings.Top;
            ViewCore.Height = settings.Height;
            ViewCore.Width = settings.Width;
        }
        ViewCore.IsMaximized = settings.IsMaximized;
    }

    public string Title => ApplicationInfo.ProductName;

    public ICommand SelectSha512Command { get; }

    public ICommand SelectSha256Command { get; }

    public ICommand SelectSha1Command { get; }

    public ICommand SelectMD5Command { get; }

    public ICommand OpenCommand { get => openCommand; set => SetProperty(ref openCommand, value); }

    public ICommand AboutCommand { get => aboutCommand; set => SetProperty(ref aboutCommand, value); }

    public object ContentView { get => contentView; set => SetProperty(ref contentView, value); }

    public HashMode HashMode { get => hashMode; set => SetProperty(ref hashMode, value); }

    public bool IsHexadecimalFormatting
    {
        get => isHexadecimalFormatting;
        set
        {
            if (!SetProperty(ref isHexadecimalFormatting, value)) return;
            IsBase64Formatting = !value;
        }
    }

    public bool IsBase64Formatting
    {
        get => isBase64Formatting;
        set
        {
            if (!SetProperty(ref isBase64Formatting, value)) return;
            IsHexadecimalFormatting = !value;
        }
    }

    public void Show() => ViewCore.Show();

    public void Close() => ViewCore.Close();

    private void ViewClosed(object? sender, EventArgs e)
    {
        settings.Left = ViewCore.Left;
        settings.Top = ViewCore.Top;
        settings.Height = ViewCore.Height;
        settings.Width = ViewCore.Width;
        settings.IsMaximized = ViewCore.IsMaximized;
    }
}
