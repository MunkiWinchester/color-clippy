using ColorClippy.Views;
using System.Windows.Input;
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
        /// Command to open the about view
        /// </summary>
        public ICommand AboutClickedCommand => new RelayCommand<MainWindow>(OpenAbout);

        public ICommand UpdateCommand => new DelegateCommand(Update.Updater.StartUpdate);

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