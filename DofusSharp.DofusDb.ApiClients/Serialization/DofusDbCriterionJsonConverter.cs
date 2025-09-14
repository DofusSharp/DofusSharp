using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

public class DofusDbCriterionJsonConverter : JsonConverter<DofusDbCriterion>
{
    #region Read

    public override DofusDbCriterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }
        reader.Read();

        List<DofusDbCriterion> currentSegment = [];
        while (reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                DofusDbCriterion criterion = Read(ref reader, typeToConvert, options) ?? throw new InvalidOperationException("Could not deserialize value.");
                currentSegment.Add(criterion);
                reader.Read();
            }
            else if (IsNextValueOperator(ref reader, out DofusDbCriterionOperator op))
            {
                // We found an operator, it means that this criterion is an operation
                reader.Read();
                DofusDbCriterion left = currentSegment.Count == 1 ? currentSegment.Single() : new DofusDbCriterionSequence(currentSegment);
                return ReadOperation(ref reader, left, op, typeToConvert, options);
            }
            else
            {
                DofusDbCriterion criterion = ReadNextStringOrResource(ref reader, options);
                currentSegment.Add(criterion);
                reader.Read();
            }
        }

        // Reaching this point means that we got through the whole array of values without encountering any operator
        return currentSegment.Count == 1 ? currentSegment.Single() : new DofusDbCriterionSequence(currentSegment);
    }

    DofusDbCriterionOperation ReadOperation(ref Utf8JsonReader reader, DofusDbCriterion left, DofusDbCriterionOperator op, Type typeToConvert, JsonSerializerOptions options)
    {
        List<DofusDbCriterion> currentSegment = [];

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                DofusDbCriterion criterion = Read(ref reader, typeToConvert, options) ?? throw new InvalidOperationException("Could not deserialize value.");
                currentSegment.Add(criterion);
                reader.Read();
            }
            else if (IsNextValueOperator(ref reader, out DofusDbCriterionOperator newOp))
            {
                reader.Read();

                int precedence = ComputePrecedence(op, newOp);

                if (precedence >= 0)
                {
                    // if left operator is stronger than right operator, it should be executed first
                    DofusDbCriterionOperation newLeft = new(left, op, currentSegment.Count == 1 ? currentSegment.Single() : new DofusDbCriterionSequence(currentSegment));
                    return ReadOperation(ref reader, newLeft, newOp, typeToConvert, options);
                }

                // else the right operator should be executed first
                DofusDbCriterion segment = currentSegment.Count == 1 ? currentSegment.Single() : new DofusDbCriterionSequence(currentSegment);
                DofusDbCriterionOperation right = ReadOperation(ref reader, segment, newOp, typeToConvert, options);
                return new DofusDbCriterionOperation(left, op, right);
            }
            else
            {
                DofusDbCriterion criterion = ReadNextStringOrResource(ref reader, options);
                currentSegment.Add(criterion);
                reader.Read();
            }

        }

        // Reaching this point means that we got through the whole array of values without encountering any operator
        return new DofusDbCriterionOperation(left, op, currentSegment.Count == 1 ? currentSegment.Single() : new DofusDbCriterionSequence(currentSegment));
    }

    static bool IsNextValueOperator(ref Utf8JsonReader reader, out DofusDbCriterionOperator op)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            op = default;
            return false;
        }

        string? value = reader.GetString();
        if (value is null || !TryDeserializeOperator(value, out op))
        {
            op = default;
            return false;
        }

        return true;
    }

    static DofusDbCriterion ReadNextStringOrResource(ref Utf8JsonReader reader, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.String => new DofusDbCriterionText(reader.GetString()),
            _ => (DofusDbCriterionResource?)JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(typeof(DofusDbCriterionResource)))
                 ?? throw new InvalidOperationException("Could not deserialize resource.")
        };

    static bool TryDeserializeOperator(string value, out DofusDbCriterionOperator op)
    {
        switch (value)
        {
            case "&":
                op = DofusDbCriterionOperator.And;
                return true;
            case "|":
                op = DofusDbCriterionOperator.Or;
                return true;
            default:
                op = default;
                return false;
        }
    }

    #endregion

    #region Write

    public override void Write(Utf8JsonWriter writer, DofusDbCriterion value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        WriteImpl(writer, value, options);
        writer.WriteEndArray();
    }

    void WriteImpl(Utf8JsonWriter writer, DofusDbCriterion value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DofusDbCriterionText text:
                if (text.Value is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(text.Value);
                }
                break;
            case DofusDbCriterionResource resource:
                JsonSerializer.Serialize(writer, resource, options.GetTypeInfo(typeof(DofusDbCriterionResource)));
                break;
            case DofusDbCriterionSequence collection:
                writer.WriteStartArray();
                foreach (DofusDbCriterion criterion in collection.Value)
                {
                    WriteImpl(writer, criterion, options);
                }
                writer.WriteEndArray();
                break;
            case DofusDbCriterionOperation operation:
                WriteOperand(writer, operation.Operator, operation.Left, options);
                writer.WriteStringValue(SerializeOperator(operation.Operator));
                WriteOperand(writer, operation.Operator, operation.Right, options);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value));

        }
    }

    /// <summary>
    ///     Write the JSON for the left operand of an operation.
    ///     This method decides whether the operand should be enclosed in an array (equivalent to parenthesis in the logical formula) or not.
    ///     The tree structure of the criterion implies that the operation described by the operand must be performed before the outer operation.
    ///     If the operand is not itself an operation: there is no ambiguity, we don't need an array.
    ///     If the operand is an operation, we ask if the operand of the inner operation has precedence over the one of the outer operation. If it does not, we need to add an array.
    /// </summary>
    void WriteOperand(Utf8JsonWriter writer, DofusDbCriterionOperator op, DofusDbCriterion operand, JsonSerializerOptions options)
    {
        bool writeInArray;
        if (operand is DofusDbCriterionOperation operation)
        {
            int precedence = ComputePrecedence(operation.Operator, op);
            writeInArray = precedence < 0;
        }
        else
        {
            writeInArray = false;
        }

        if (writeInArray)
        {
            writer.WriteStartArray();
        }

        WriteImpl(writer, operand, options);

        if (writeInArray)
        {
            writer.WriteEndArray();
        }
    }

    /// <summary>
    ///     Compute the precedence between the two operators. The output is 1 if <see cref="op1" /> has precedence over <see cref="op2" />, 0 if they have the same precedence and -1
    ///     if <see cref="op2" /> has precedence over <see cref="op1" />
    /// </summary>
    int ComputePrecedence(DofusDbCriterionOperator op1, DofusDbCriterionOperator op2)
    {
        if (op1 == op2)
        {
            return 0;
        }

        return (op1, op2) switch
        {
            (DofusDbCriterionOperator.And, DofusDbCriterionOperator.Or) => 1,
            (DofusDbCriterionOperator.Or, DofusDbCriterionOperator.And) => -1,
            _ => 0
        };
    }

    string SerializeOperator(DofusDbCriterionOperator op) =>
        op switch
        {
            DofusDbCriterionOperator.And => "&",
            DofusDbCriterionOperator.Or => "|",
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };

    #endregion
}
