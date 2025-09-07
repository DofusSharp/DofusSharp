using Semver;

namespace BestCrush.Domain.Services.Upgrades;

public interface IApplicationUpgrader
{
    SemVersion TargetVersion { get; }
    Task UpgradeAsync(SemVersion? oldVersion, SemVersion newVersion, CancellationToken cancellationToken = default);
}
