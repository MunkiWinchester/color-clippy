using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media;

namespace ColorClippy.DataObjects
{
    public class ColorCollection
    {
        public string Name { get; set; }
        public List<ColorItem> Items { get; set; }

    }

    public class ColorItem
    {
        public string HexCode { get; private set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public SolidColorBrush Color { get; private set; }

        public ColorItem(string name, string hexCode)
        {
            Name = name;
            HexCode = hexCode;

            Color = (SolidColorBrush)(new BrushConverter().ConvertFrom(hexCode));
        }
    }
}
