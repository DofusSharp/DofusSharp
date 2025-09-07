using BestCrush.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BestCrush.Domain.Services.Upgrades;

public class ApplicationUpgradesHandler(BestCrushDbContext dbContext, IEnumerable<IApplicationUpgrader> upgraders, ILogger<ApplicationUpgradesHandler> logger)
{
    public async Task UpgradeAsync(Version newVersion, ProgressSync<ProgressMessage>? progress = null, CancellationToken cancellationToken = default)
    {
        Upgrade? lastUpgrade = await dbContext.Upgrades.Where(u => u.Kind == UpgradeKind.Application).OrderByDescending(u => u.UpgradeDate).FirstOrDefaultAsync(cancellationToken);
        Version? oldVersion = Version.TryParse(lastUpgrade?.NewVersion, out Version? version) ? version : null;

        if (oldVersion == newVersion)
        {
            logger.LogInformation("Application is up to date. Version: {Version}.", newVersion);
            return;
        }

        logger.LogInformation("Running upgrade from version {OldVersion} to {NewVersion}...", oldVersion, newVersion);

        IApplicationUpgrader[] toRun = upgraders
            .Where(upgrader => upgrader.TargetVersion > oldVersion && upgrader.TargetVersion <= newVersion)
            .OrderBy(upgrader => upgrader.TargetVersion)
            .ToArray();
        if (toRun.Length > 0)
        {
            for (int index = 0; index < toRun.Length; index++)
            {
                progress?.ReportStep("Running upgraders", index, toRun.Length);

                IApplicationUpgrader upgrader = toRun[index];
                logger.LogInformation("Running upgrader {Upgrader}...", upgrader);
                await upgrader.UpgradeAsync(oldVersion, newVersion, cancellationToken);
            }
            progress?.ReportStep("Running upgraders", toRun.Length, toRun.Length);
        }
        else
        {
            logger.LogInformation("No upgrader to run.");
        }

        Upgrade newUpgrade = new() { Kind = UpgradeKind.Application, OldVersion = oldVersion?.ToString(), NewVersion = newVersion.ToString(), UpgradeDate = DateTime.Now };
        dbContext.Upgrades.Add(newUpgrade);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully executed a total of {UpgradersCount} upgraders.", toRun.Length);
    }
}
