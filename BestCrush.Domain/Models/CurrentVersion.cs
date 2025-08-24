using System.ComponentModel.DataAnnotations;

namespace BestCrush.EfCore.Models;

public class CurrentVersion
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public CurrentVersion() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public CurrentVersion(string dofusDbVersion)
    {
        DofusDbVersion = dofusDbVersion;
    }

    public Guid Id { get; private set; }

    [MaxLength(64)]
    public string DofusDbVersion { get; set; }
}
