using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     A type of items.
/// </summary>
public class DofocusType
{
    /// <summary>
    ///     The unique identifier of the type.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    ///     The localized name of the type.
    /// </summary>
    public required DofocusMultiLangString Name { get; init; }
}
