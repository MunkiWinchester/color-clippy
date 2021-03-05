using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Squirrel;
using ColorClippy.Business;
using ColorClippy.Views;

namespace ColorClippy.Update
{
    internal static partial class Updater
    {
        private static ReleaseUrls _releaseUrls;
        private static TimeSpan _updateCheckDelay = new TimeSpan(0, 25, 0);
        private static bool ShouldCheckForUpdates()
            => DateTime.Now - _lastUpdateCheck >= _updateCheckDelay;

        public static async void CheckForUpdates(bool force = false)
        {
            if(!force && !ShouldCheckForUpdates())
                return;
            _lastUpdateCheck = DateTime.Now;
            try
            {
                bool updated;
                using (var mgr = await GetUpdateManager())
                {
                    updated = await SquirrelUpdate(mgr, null);
                }

                if(updated)
                    StatusBar.Visibility = Visibility.Visible;
            }
            catch(Exception e)
            {
                Logger.Error("CheckForUpdates(bool force = false)", e);
            }
        }

        private static string GetReleaseUrl(string release)
        {
            if (_releaseUrls != null)
                return _releaseUrls.GetReleaseUrl(release);
            else
                _releaseUrls = new ReleaseUrls();
            var url = _releaseUrls.GetReleaseUrl(release);
            Logger.Info($"Using '{release}' release: {url}");
            return url;
        }

        private static async Task<UpdateManager> GetUpdateManager()
        {
            // https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/using/github.md
            return await UpdateManager.GitHubUpdateManager(GetReleaseUrl("live"));
        }

        public static async Task StartupUpdateCheck(SplashScreenWindow splashScreenWindow)
        {
            try
            {
                Logger.Info("Checking for updates");
                bool updated;
                using(var mgr = await GetUpdateManager())
                {
                    SquirrelAwareApp.HandleEvents(
                        v =>
                        {
                            mgr.CreateShortcutForThisExe();
                        },
                        v =>
                        {
                            mgr.CreateShortcutForThisExe();
                        },
                        onAppUninstall: v =>
                        {
                            mgr.RemoveShortcutForThisExe();
                        });
                    updated = await SquirrelUpdate(mgr, splashScreenWindow);
                }

                if (updated)
                {
                    Logger.Info("Update complete, restarting");
                    UpdateManager.RestartApp();
                }
            }
            catch(Exception e)
            {
                Logger.Error("StartupUpdateCheck(SplashScreenWindow splashScreenWindow)", e);
            }
        }

        private static async Task<bool> SquirrelUpdate(UpdateManager mgr, SplashScreenWindow splashScreenWindow, bool ignoreDelta = false)
        {
            try
            {
                var updateInfo = await mgr.CheckForUpdate(ignoreDelta);
                if(!updateInfo.ReleasesToApply.Any())
                {
                    Logger.Info("No new updated available");
                    return false;
                }
                var latest = updateInfo.ReleasesToApply.LastOrDefault()?.Version;
                var current = mgr.CurrentlyInstalledVersion();
                if(latest <= current)
                {
                    Logger.Info($"Installed version ({current}) is greater or equal ({latest}). Not downloading updates.");
                    return false;
                }
                if(IsRevisionIncrement(current?.Version, latest?.Version))
                {
                    Logger.Info($"Newest version ({latest}) is greater or equal ({current}). Updating in background.");
                }
                if(splashScreenWindow != null)
                    await mgr.DownloadReleases(updateInfo.ReleasesToApply, splashScreenWindow.Updating);
                else
                    await mgr.DownloadReleases(updateInfo.ReleasesToApply);
                splashScreenWindow?.Updating(100);
                Logger.Info($"Applying release ({latest})");
                if(splashScreenWindow != null)
                    await mgr.ApplyReleases(updateInfo, splashScreenWindow.Installing);
                else
                    await mgr.ApplyReleases(updateInfo);
                splashScreenWindow?.Installing(100);
                await mgr.CreateUninstallerRegistryEntry();
                Logger.Info("Applying done");
                return true;
            }
            catch(Exception e)
            {
                if(ignoreDelta)
                    return false;
                if(e is Win32Exception)
                    Logger.Error("Not able to apply deltas, downloading full release", e);
                if (e is Exception)
                    Logger.Error("Not able to apply update", e);
                return await SquirrelUpdate(mgr, splashScreenWindow, true);
            }
        }

        private static bool IsRevisionIncrement(Version current, Version latest)
        {
            if(current == null || latest == null)
                return false;
            return  current.Major == latest.Major &&
                    current.Minor == latest.Minor &&
                    current.Build <  latest.Build;
        }

        internal static void StartUpdate()
        {
            Logger.Info("Restarting...");
            UpdateManager.RestartApp();
        }
    }
}
