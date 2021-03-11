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
                new ColorItem("grau", "#212121"),
                new ColorItem("red", "#ff0000"),
                new ColorItem("green", "#00ff00"),
                new ColorItem("blue", "#0000ff")
            };
            var l = new List<ColorCollection>
            {
                new ColorCollection
                {
                    Name = "Test",
                    Items = i
                },
                new ColorCollection
                {
                    Name = "Test 2",
                    Items = i
                }
            };
            this.MySearchItems = new ObservableCollection<ColorCollection>(l);
        }
    }
}
