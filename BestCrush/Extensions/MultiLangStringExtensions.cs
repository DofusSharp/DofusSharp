using System.Globalization;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace BestCrush.Extensions;

static class MultiLangStringExtensions
{
    public static string Localize(this DofusDbMultiLangString str)
    {
        CultureInfo culture = CultureInfo.CurrentUICulture;
        if (culture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase))
        {
            return str.En ?? "";
        }

        if (culture.Name.StartsWith("fr", StringComparison.OrdinalIgnoreCase))
        {
            return str.Fr ?? "";
        }

        if (culture.Name.StartsWith("es", StringComparison.OrdinalIgnoreCase))
        {
            return str.Es ?? "";
        }

        if (culture.Name.StartsWith("pt", StringComparison.OrdinalIgnoreCase))
        {
            return str.Pt ?? "";
        }

        if (culture.Name.StartsWith("de", StringComparison.OrdinalIgnoreCase))
        {
            return str.De ?? "";
        }

        return str.En ?? "";
    }

    public static string Localize(this DofocusMultiLangString str)
    {
        CultureInfo culture = CultureInfo.CurrentUICulture;
        if (culture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase))
        {
            return str.En ?? "";
        }

        if (culture.Name.StartsWith("fr", StringComparison.OrdinalIgnoreCase))
        {
            return str.Fr ?? "";
        }

        if (culture.Name.StartsWith("es", StringComparison.OrdinalIgnoreCase))
        {
            return str.Es ?? "";
        }

        return str.En ?? "";
    }
}
