namespace BestCrush.Domain.Models;

public record struct AsyncProgress(string Message, double? Progress, bool Done = false);
