using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ANTI_CHEAT_BYPASSER
{
    public class FNInstalled
    {
        public static string GetFNPath()
        {
            var launcherInstalledDat = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            var installationList = JsonConvert.DeserializeObject<EpicInstallLocations>(File.ReadAllText(launcherInstalledDat)).InstallationList;
            var installation = installationList.FirstOrDefault((Installation i) => i.AppName == "Fortnite");
            var fnPath = installation.InstallLocation;
            return fnPath;
        }
        public static string GetFNVersion()
        {
            var launcherInstalledDat = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            var installationList = JsonConvert.DeserializeObject<EpicInstallLocations>(File.ReadAllText(launcherInstalledDat)).InstallationList;
            var installation = installationList.FirstOrDefault((Installation i) => i.AppName == "Fortnite");
            var fnVersion = installation.AppVersion;
            return fnVersion;
        }
    }
    public class EpicInstallLocations
    {
        public List<Installation> InstallationList { get; set; }
    }
    public class Installation
    {
        public string InstallLocation { get; set; }
        public string NamespaceId { get; set; }
        public string ItemId { get; set; }
        public string ArtifactId { get; set; }
        public string AppVersion { get; set; }
        public string AppName { get; set; }
    }
}
