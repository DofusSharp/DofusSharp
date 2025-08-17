namespace DofusSharp.DofusDb.ApiClients.Models.Common;

/// <summary>
///     Coordinates in a 2D grid.
/// </summary>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
public record struct DoubleCoordinates(double X, double Y);
