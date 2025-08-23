using System.Globalization;
using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace BestCrush.Extensions;

static class DofocusMultiStringLanguageExtensions
{
    public static string Localize(this DofocusMultiLangString str)
    {
        CultureInfo culture = CultureInfo.CurrentUICulture;
        return culture.Name switch
        {
            "en-US" or "en" => str.En,
            "fr-FR" or "fr" => str.Fr,
            "es-ES" or "es" => str.Es,
            _ => str.En
        };
    }
}
