using System.ComponentModel.DataAnnotations;

namespace BestCrush.Domain.Models;

public class Upgrade
{
    public Guid Id { get; private set; }

    public required UpgradeKind Kind { get; init; }

    [MaxLength(128)]
    public string? OldVersion { get; init; }

    [MaxLength(128)]
    public required string NewVersion { get; init; }

    public required DateTime UpgradeDate { get; init; }
}
