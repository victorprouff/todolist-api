using System.Data;
using Dapper;
using NodaTime;

namespace Todolist.Api.Data.Handlers;

public class InstantHandler : SqlMapper.TypeHandler<Instant>
{
    public override void SetValue(IDbDataParameter parameter, Instant value)
    {
        parameter.Value = value;
    }

    // This is not necessary since Npgsql already provide the correct typed value
    public override Instant Parse(object value)
    {
        if (value is not LocalDateTime time)
        {
            return (Instant)value;
        }

        var timezone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
        var zonedTime = time.InZoneStrictly(timezone);
        return zonedTime.ToInstant();
    }
}