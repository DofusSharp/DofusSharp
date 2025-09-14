namespace DofusSharp.DofusDb.ApiClients.Models.Items;

/// <inheritdoc cref="DofusDbItem" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for items: the className fields is ItemData instead of Items for the prod environment.
///     This model is an exact copy of <see cref="DofusDbItem" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbItemBeta : DofusDbItem;
