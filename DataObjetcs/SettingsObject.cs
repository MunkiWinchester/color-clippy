namespace ColorClippy.DataObjects
{
    public class SettingsObject
    {
        public double Top { get; set; }
        public double Left { get; set; }

        public SettingsObject()
        {
        }

        public SettingsObject(bool useDefaults)
        {
            if (useDefaults) {
                Top = 50;
                Left = 50;
            }
        }
    }
}
