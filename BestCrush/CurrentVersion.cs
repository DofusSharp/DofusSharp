using System.Diagnostics;
using System.Reflection;
using Semver;

namespace BestCrush;

static class CurrentVersion
{
    public static SemVersion Version { get; }

    static CurrentVersion()
    {
        Assembly thisAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        string path = thisAssembly.Location;
        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
        Version = versionInfo.ProductVersion is null ? new SemVersion(0, 0, 0) : SemVersion.Parse(versionInfo.ProductVersion);
    }
}
