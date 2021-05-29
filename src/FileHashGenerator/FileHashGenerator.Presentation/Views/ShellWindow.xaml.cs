using System;
using System.Windows;
using System.Windows.Input;
using Waf.FileHashGenerator.Applications.Views;
using System.ComponentModel.Composition;

namespace Waf.FileHashGenerator.Presentation.Views
{
    [Export, Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public double VirtualScreenWidth => SystemParameters.VirtualScreenWidth;

        public double VirtualScreenHeight => SystemParameters.VirtualScreenHeight;

        public bool IsMaximized
        {
            get => WindowState == WindowState.Maximized;
            set
            {
                if (value)
                {
                    WindowState = WindowState.Maximized;
                }
                else if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
            }
        }

        private void ViewPopupOpened(object sender, EventArgs e) => hexadecimalButton.Focus();

        private void ViewPopupKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) viewPopup.IsOpen = false;
        }
    }
}
