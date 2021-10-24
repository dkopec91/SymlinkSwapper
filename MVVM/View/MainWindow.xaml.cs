using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace SymlinkSwapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            this.MouseDown += Window_MouseDown;

            _notifyIcon = new()
            {
                Text = "SymlinkSwapper",
                BalloonTipTitle = "SymlinkSwapper",
                BalloonTipText = "The app has been minimised. Click the tray icon to show.",
                Icon = new Icon(Properties.Resources.AppIcon, new System.Drawing.Size(16, 16))
            };
            _notifyIcon.Click += new EventHandler(_notifyIcon_Click);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (_notifyIcon != null)
                _notifyIcon.ShowBalloonTip(500);
        }

        private void _notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnClose(object sender, CancelEventArgs args)
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = !IsVisible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
