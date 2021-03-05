using System;

namespace ColorClippy.Update
{
    internal static partial class Updater
    {
        private static DateTime _lastUpdateCheck = DateTime.Now;
        public static StatusBarHelper StatusBar { get; } = new StatusBarHelper();
    }
}
