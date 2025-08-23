using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     A super type of items.
/// </summary>
public class DofocusSuperType
{
    /// <summary>
    ///     The unique identifier of the super type.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    ///     The localized name of the super type.
    /// </summary>
    public required DofocusMultiLangString Name { get; init; }
}
