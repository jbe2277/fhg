using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Views;

namespace Waf.FileHashGenerator.Applications.ViewModels
{
    [Export]
    public class ShellViewModel : ViewModel<IShellView>
    {
        private ICommand openCommand;
        private ICommand aboutCommand;
        private object contentView;
        private HashMode hashMode;
        private bool isHexadecimalFormatting;
        private bool isBase64Formatting;


        [ImportingConstructor]
        public ShellViewModel(IShellView view) : base(view)
        {
            SelectSha512Command = new DelegateCommand(() => HashMode = HashMode.Sha512);
            SelectSha256Command = new DelegateCommand(() => HashMode = HashMode.Sha256);
            SelectSha1Command = new DelegateCommand(() => HashMode = HashMode.Sha1);
            SelectMD5Command = new DelegateCommand(() => HashMode = HashMode.MD5);
            isHexadecimalFormatting = true;

            view.Closed += ViewClosed;

            // Restore the window size when the values are valid.
            if (Settings.Default.Left >= 0 && Settings.Default.Top >= 0 && Settings.Default.Width > 0 && Settings.Default.Height > 0
                && Settings.Default.Left + Settings.Default.Width <= view.VirtualScreenWidth
                && Settings.Default.Top + Settings.Default.Height <= view.VirtualScreenHeight)
            {
                ViewCore.Left = Settings.Default.Left;
                ViewCore.Top = Settings.Default.Top;
                ViewCore.Height = Settings.Default.Height;
                ViewCore.Width = Settings.Default.Width;
            }
            ViewCore.IsMaximized = Settings.Default.IsMaximized;
        }


        public string Title => ApplicationInfo.ProductName;

        public ICommand SelectSha512Command { get; }

        public ICommand SelectSha256Command { get; }

        public ICommand SelectSha1Command { get; }

        public ICommand SelectMD5Command { get; }

        public ICommand OpenCommand
        {
            get => openCommand;
            set => SetProperty(ref openCommand, value);
        }

        public ICommand AboutCommand
        {
            get => aboutCommand;
            set => SetProperty(ref aboutCommand, value);
        }

        public object ContentView
        {
            get => contentView;
            set => SetProperty(ref contentView, value);
        }

        public HashMode HashMode
        {
            get => hashMode;
            set => SetProperty(ref hashMode, value);
        }

        public bool IsHexadecimalFormatting
        {
            get => isHexadecimalFormatting;
            set
            {
                if (SetProperty(ref isHexadecimalFormatting, value))
                {
                    IsBase64Formatting = !value;
                }
            }
        }

        public bool IsBase64Formatting
        {
            get => isBase64Formatting;
            set
            {
                if (SetProperty(ref isBase64Formatting, value))
                {
                    IsHexadecimalFormatting = !value;
                }
            }
        }

        public void Show()
        {
            ViewCore.Show();
        }

        public void Close()
        {
            ViewCore.Close();
        }

        private void ViewClosed(object sender, EventArgs e)
        {
            Settings.Default.Left = ViewCore.Left;
            Settings.Default.Top = ViewCore.Top;
            Settings.Default.Height = ViewCore.Height;
            Settings.Default.Width = ViewCore.Width;
            Settings.Default.IsMaximized = ViewCore.IsMaximized;
        }
    }
}
