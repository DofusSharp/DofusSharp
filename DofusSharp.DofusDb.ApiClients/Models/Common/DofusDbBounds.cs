namespace DofusSharp.DofusDb.ApiClients.Models.Common;

/// <summary>
///     Rectangular bounds in a 2D grid.
/// </summary>
/// <param name="X">The X coordinate of the top-left corner.</param>
/// <param name="Y">The Y coordinate of the top-left corner.</param>
/// <param name="Width">The width of the rectangle.</param>
/// <param name="Height">The height of the rectangle.</param>
public record struct DofusDbBounds(int X, int Y, int Width, int Height);
