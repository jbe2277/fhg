using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Waf.FileHashGenerator.Applications.Views;
using System.ComponentModel.Composition;
using System.Windows.Shell;

namespace Waf.FileHashGenerator.Presentation.Views
{
    [Export, Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        public ShellWindow()
        {
            InitializeComponent();
        }


        public double VirtualScreenWidth { get { return SystemParameters.VirtualScreenWidth; } }

        public double VirtualScreenHeight { get { return SystemParameters.VirtualScreenHeight; } }

        public bool IsMaximized
        {
            get { return WindowState == WindowState.Maximized; }
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

        private void ViewPopupOpened(object sender, EventArgs e)
        {
            hexadecimalButton.Focus();
        }

        private void ViewPopupKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                viewPopup.IsOpen = false;
            }
        }
    }
}
