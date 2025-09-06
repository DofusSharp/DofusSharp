namespace BestCrush.Domain.Services.Upgrades;

public interface IApplicationUpgrader
{
    Version TargetVersion { get; }
    Task UpgradeAsync(Version? oldVersion, Version newVersion, CancellationToken cancellationToken = default);
}
