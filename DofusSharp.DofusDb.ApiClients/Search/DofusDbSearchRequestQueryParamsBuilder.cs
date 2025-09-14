using System.Text;
using System.Web;

namespace DofusSharp.DofusDb.ApiClients.Search;

class DofusDbSearchRequestQueryParamsBuilder
{
    public string BuildQueryParams(DofusDbSearchQuery query)
    {
        QueryParamsBuilder builder = new();

        if (query.Limit is not null)
        {
            builder.Add("$limit", $"{query.Limit}");
        }

        if (query.Skip is not null)
        {
            builder.Add("$skip", $"{query.Skip}");
        }

        foreach ((string sortParam, DofusDbSearchQuerySortOrder sortOrder) in query.Sort)
        {
            switch (sortOrder)
            {
                case DofusDbSearchQuerySortOrder.Ascending:
                    builder.Add($"$sort[{sortParam.ToCamelCase()}]", "1");
                    break;
                case DofusDbSearchQuerySortOrder.Descending:
                    builder.Add($"$sort[{sortParam.ToCamelCase()}]", "-1");
                    break;
            }
        }

        foreach (string selectParam in query.Select)
        {
            builder.Add("$select[]", selectParam.ToCamelCase());
        }

        foreach (DofusDbSearchPredicate predicate in query.Predicates)
        {
            AddPredicateParams(builder, predicate, []);
        }

        return builder.Build();
    }

    static void AddPredicateParams(QueryParamsBuilder builder, DofusDbSearchPredicate queryPredicate, string[] path)
    {
        switch (queryPredicate)
        {
            case DofusDbSearchPredicate.Eq p:
                builder.Add(FormatNestedFieldName([..path, p.Field]), p.Value);
                break;
            case DofusDbSearchPredicate.NotEq p:
                builder.Add(FormatNestedFieldName([..path, p.Field, "$ne"]), p.Value);
                break;
            case DofusDbSearchPredicate.In p:
                foreach (string value in p.Value)
                {
                    builder.Add(FormatNestedFieldName([..path, p.Field, "$in", ""]), value);
                }
                break;
            case DofusDbSearchPredicate.NotIn p:
                foreach (string value in p.Value)
                {
                    builder.Add(FormatNestedFieldName([..path, p.Field, "$nin", ""]), value);
                }
                break;
            case DofusDbSearchPredicate.GreaterThan p:
                builder.Add(FormatNestedFieldName([..path, p.Field, "$gt"]), p.Value);
                break;
            case DofusDbSearchPredicate.GreaterThanOrEqual p:
                builder.Add(FormatNestedFieldName([..path, p.Field, "$gte"]), p.Value);
                break;
            case DofusDbSearchPredicate.LessThan p:
                builder.Add(FormatNestedFieldName([..path, p.Field, "$lt"]), p.Value);
                break;
            case DofusDbSearchPredicate.LessThanOrEquals p:
                builder.Add(FormatNestedFieldName([..path, p.Field, "$lte"]), p.Value);
                break;
            case DofusDbSearchPredicate.And p:
                for (int index = 0; index < p.Predicates.Count; index++)
                {
                    AddPredicateParams(builder, p.Predicates[index], [..path, "$and", $"{index}"]);
                }
                break;
            case DofusDbSearchPredicate.Or p:
                for (int index = 0; index < p.Predicates.Count; index++)
                {
                    AddPredicateParams(builder, p.Predicates[index], [..path, "$or", $"{index}"]);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(queryPredicate));
        }
    }

    static string FormatNestedFieldName(params string[] path) =>
        path.Length == 0 ? "" : $"{path[0].ToCamelCase()}{string.Join("", path.Skip(1).Select(p => $"[{p.ToCamelCase()}]"))}";

    class QueryParamsBuilder
    {
        readonly StringBuilder _stringBuilder = new();

        public QueryParamsBuilder Add(string key, string value)
        {
            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.Append('&');
            }

            _stringBuilder.Append($"{key}={HttpUtility.UrlEncode(value)}");
            return this;
        }

        public string Build() => _stringBuilder.ToString();
    }
}
