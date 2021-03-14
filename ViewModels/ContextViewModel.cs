using ColorClippy.DataObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfUtility.Services;

namespace ColorClippy.ViewModels
{
    public class ContextViewModel : ObservableObject
    {
        public ICommand AddUserCommand => new DelegateCommand(Close);
        public ObservableCollection<ColorCollection> MySearchItems { get; set; }

        private readonly MainWindowViewModel _mwvm;

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ContextViewModel()
        {
            _mwvm = new MainWindowViewModel();
            MySearchItems = new ObservableCollection<ColorCollection>(_mwvm.LoadColorCollections());
        }
    }
}
