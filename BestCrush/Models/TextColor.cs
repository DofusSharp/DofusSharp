namespace BestCrush.Models;

public enum TextColor
{
    Default,
    Danger,
    Success
}

static class TextColorExtensions
{
    public static string ToClass(this TextColor color) =>
        color switch
        {
            TextColor.Success => "text-success",
            TextColor.Danger => "text-danger",
            TextColor.Default or _ => ""
        };
}
