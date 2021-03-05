using System;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using ColorClippy.Business;
using ColorClippy.DataObjetcs;
using ColorClippy.Views;
using WpfUtility.Services;

namespace ColorClippy.ViewModels
{
    public class MainWindowViewModel
    {
        /// <summary>
        /// Initializes a main window view model
        /// </summary>
        public MainWindowViewModel()
        {
        }

        /// <summary>
        /// Command to open the settings
        /// </summary>
        public ICommand SettingsClickedCommand => new RelayCommand<MainWindow>(OpenSettings);

        /// <summary>
        /// Command to open the about view
        /// </summary>
        public ICommand AboutClickedCommand => new RelayCommand<MainWindow>(OpenAbout);

        public ICommand UpdateCommand => new DelegateCommand(Update.Updater.StartUpdate);

        /// <summary>
        /// Opens the settings window centered on the main window
        /// </summary>
        /// <param name="mainWindow"></param>
        private void OpenSettings(MainWindow mainWindow)
        {
            var settings = new SettingsWindow(mainWindow);
            settings.ShowDialog();
        }

        /// <summary>
        /// Opens the about window centered on the main window
        /// </summary>
        /// <param name="mainWindow"></param>
        private static void OpenAbout(MainWindow mainWindow)
        {
            var about = new AboutWindow(mainWindow);
            about.ShowDialog();
        }
    }
}