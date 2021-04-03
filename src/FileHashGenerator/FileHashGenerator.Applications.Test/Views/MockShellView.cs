﻿using System;
using System.ComponentModel.Composition;
using System.Waf.UnitTesting.Mocks;
using Waf.FileHashGenerator.Applications.Views;

namespace Test.FileHashGenerator.Applications.Views
{
    [Export(typeof(IShellView)), Export]
    public class MockShellView : MockView, IShellView
    {
        public bool IsVisible { get; private set; }

        public double VirtualScreenWidth { get; set; }

        public double VirtualScreenHeight { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool IsMaximized { get; set; }


        public event EventHandler? Closed;


        public void Show()
        {
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
            OnClosed(EventArgs.Empty);
        }

        public void SetNAForLocationAndSize()
        {
            Top = double.NaN;
            Left = double.NaN;
            Width = double.NaN;
            Height = double.NaN;
        }

        protected virtual void OnClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }
    }
}
