using ColorClippy.Views;
using System.Windows.Input;
using WpfUtility.Services;
using ColorClippy.DataObjects;
using System.Collections.Generic;
using ColorClippy.Business;
using System.IO;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Linq;

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

        public List<ColorCollection> LoadColorCollections()
        {
            var conDic = new ConcurrentDictionary<string, ColorCollection>();
            try
            {
                var dirInfo = Helper.GetJsonSaveLocation();
                if (dirInfo != null)
                {
                    var fileInfos = dirInfo.GetFiles("*.json", SearchOption.TopDirectoryOnly);
                    Parallel.ForEach(fileInfos,
                        fileInfo =>
                        {
                            var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                            if (!conDic.ContainsKey(name))
                            {
                                var collections = JsonConvert.DeserializeObject<List<ColorCollection>>(File.ReadAllText(fileInfo.FullName));
                                collections.ForEach((coll) =>
                                {
                                    conDic.TryAdd(coll.Name, coll);
                                });
                            }
                        });
                }
            }
            catch (Exception e)
            {
                Logger.Error("Problem while loading values.", e);
            }
            if (!conDic.Values.Any())
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
                SaveValues(l);

                l.ForEach((coll) =>
                {
                    conDic.TryAdd(coll.Name, coll);
                });
            }

            return conDic.Values.ToList();
        }

        public void SaveValues(List<ColorCollection> coll)
        {
            var json = JsonConvert.SerializeObject(coll,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.Indented,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
            var dirInfo = Helper.GetJsonSaveLocation();
            if (dirInfo != null)
                using (var sw = new StreamWriter(Path.Combine(dirInfo.FullName, "ColorCollections.json")))
                {
                    sw.Write(json);
                }
        }
    }
}