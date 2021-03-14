using System;
using System.IO;
using System.Reflection;

namespace ColorClippy.Business
{
    public static class Helper
    {
        public static DirectoryInfo GetJsonSaveLocation()
        {
            var location = Path.Combine(GetBaseSaveDirectory(), "ColorCollections");
            if (!Directory.Exists(location))
                Directory.CreateDirectory(location);
            DirectoryInfo dirInfo = new DirectoryInfo(location);
            return dirInfo;
        }

        public static string GetBaseSaveDirectory()
        {
            var location = string.Empty;
#if DEBUG
            location = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Test");
#else
            location = Path.Combine(
                            Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)),
                            $".{Assembly.GetExecutingAssembly().GetName().Name}");
#endif
            if (!Directory.Exists(location))
                Directory.CreateDirectory(location);
            return location;
        }

        public static Version GetCurrentVersion() => Assembly.GetExecutingAssembly().GetName().Version;

        public static string ToVersionString(this Version version, bool includeRef = false) =>
            $"{version?.Major}.{version?.Minor}.{version?.Build}{(includeRef ? "." + version?.Revision : "")}";

        public static string LoadAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
