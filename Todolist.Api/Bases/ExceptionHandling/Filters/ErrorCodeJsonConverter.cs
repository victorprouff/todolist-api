using System.Text.Json;
using System.Text.Json.Serialization;

namespace Todolist.Api.Bases.ExceptionHandling.Filters;

public class ErrorCodeJsonConverter : JsonConverter<ErrorCode>
{
    public override ErrorCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("You should not read a json with error code");
    }

    public override void Write(Utf8JsonWriter writer, ErrorCode errorCode, JsonSerializerOptions options)
    {
        writer.WriteStringValue(errorCode.Code);
    }
}