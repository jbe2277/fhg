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
        private readonly DelegateCommand selectSha512Command;
        private readonly DelegateCommand selectSha256Command;
        private readonly DelegateCommand selectSha1Command;
        private readonly DelegateCommand selectMD5Command;
        private ICommand openCommand;
        private ICommand aboutCommand;
        private object contentView;
        private HashMode hashMode;
        private bool isHexadecimalFormatting;
        private bool isBase64Formatting;


        [ImportingConstructor]
        public ShellViewModel(IShellView view) : base(view)
        {
            this.selectSha512Command = new DelegateCommand(() => HashMode = HashMode.Sha512);
            this.selectSha256Command = new DelegateCommand(() => HashMode = HashMode.Sha256);
            this.selectSha1Command = new DelegateCommand(() => HashMode = HashMode.Sha1);
            this.selectMD5Command = new DelegateCommand(() => HashMode = HashMode.MD5);
            this.isHexadecimalFormatting = true;

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


        public string Title { get { return ApplicationInfo.ProductName; } }

        public ICommand SelectSha512Command { get { return selectSha512Command; } }

        public ICommand SelectSha256Command { get { return selectSha256Command; } }

        public ICommand SelectSha1Command { get { return selectSha1Command; } }

        public ICommand SelectMD5Command { get { return selectMD5Command; } }

        public ICommand OpenCommand
        {
            get { return openCommand; }
            set { SetProperty(ref openCommand, value); }
        }

        public ICommand AboutCommand
        {
            get { return aboutCommand; }
            set { SetProperty(ref aboutCommand, value); }
        }

        public object ContentView
        {
            get { return contentView; }
            set { SetProperty(ref contentView, value); }
        }

        public HashMode HashMode
        {
            get { return hashMode; }
            set { SetProperty(ref hashMode, value); }
        }

        public bool IsHexadecimalFormatting
        {
            get { return isHexadecimalFormatting; }
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
            get { return isBase64Formatting; }
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
