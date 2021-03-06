using ColorClippy.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfUtility.Services;

namespace ColorClippy.ViewModels
{
    public class ContextViewModel : ObservableObject
    {
        public ICommand AddUserCommand => new DelegateCommand(Close);
        public ObservableCollection<ColorCollection> MySearchItems { get; set; }

        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ContextViewModel()
        {
            var i = new List<ColorItem>
            {
                new ColorItem("grau", "#212121")
            };
            var coll = new ColorCollection
            {
                Name = "Test",
                Items = i
            };
            var l = new List<ColorCollection>
            {
                coll
            };
            this.MySearchItems = new ObservableCollection<ColorCollection>(l);
        }
    }
}
