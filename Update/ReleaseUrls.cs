namespace ColorClippy.Update
{
    public class ReleaseUrls
    {
        private const string FieldLive = "live";

        public string Live => "https://github.com/munkiwinchester/ColorClippy";

        public string GetReleaseUrl(string release) => Live;
    }
}
