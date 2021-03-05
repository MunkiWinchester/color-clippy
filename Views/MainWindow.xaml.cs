using ColorClippy.Properties;
using ColorClippy.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfUtility.Services;
using Settings = ColorClippy.Business.Settings;

namespace ColorClippy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        /// <summary>
        /// Contains the view model
        /// </summary>
        private MainWindowViewModel _viewModel = new MainWindowViewModel();

        private TaskbarIcon _taskbarIcon;

        public MainWindow()
        {
            InitializeComponent();

            _taskbarIcon = (TaskbarIcon)FindResource("TaskbarIcon");
            _taskbarIcon.TrayPopup = new Views.Tray.ContextMenu();
        }

        internal void LoadConfigSettings()
        {
            Top = Settings.Default.Top;
            Left = Settings.Default.Left;

            DataContext = _viewModel;
            Update.Updater.StatusBar.PropertyChanged += StatusBar_PropertyChanged;

            _taskbarIcon.DoubleClickCommand = new DelegateCommand(() => NotifyIconOnClick(false));
            if (_taskbarIcon.TrayPopup is Views.Tray.ContextMenu contextMenu)
            {
                contextMenu.CloseCommand = new DelegateCommand(Close);
                contextMenu.MinimizeShowCommand = new DelegateCommand(() => NotifyIconOnClick(WindowState == WindowState.Normal));
                contextMenu.AboutCommand = _viewModel.AboutClickedCommand;
            }
        }

        private void StatusBar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Update.StatusBarHelper statusBar && statusBar.Visibility == Visibility.Visible)
                _taskbarIcon.ShowBalloonTip("New Update Available!", $@"There is a new update available.{
                    Environment.NewLine}Please restart the application.", BalloonIcon.None);
        }

        /// <summary>
        /// Shows the window (from the system tray)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void NotifyIconOnClick(bool minimizeToTray)
        {
            var contextMenu = _taskbarIcon.TrayPopup as Views.Tray.ContextMenu;

            if (!minimizeToTray)
            {
                // yes doubled entry, but it ain't stupid if it works
                WindowState = WindowState.Normal;
                Activate();
                Show();
                WindowState = WindowState.Normal;

                if (contextMenu != null)
                    contextMenu.IsMinimized = false;
            }
            else
            {
                Hide();
                WindowState = WindowState.Minimized;

                if (contextMenu != null)
                    contextMenu.IsMinimized = true;
            }
        }

        /// <summary>
        /// Saves the position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Settings.Default.Top = Top;
            Settings.Default.Left = Left;
            Settings.Save();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
