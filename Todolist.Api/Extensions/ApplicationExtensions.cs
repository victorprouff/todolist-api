using Autofac;
using NodaTime;
using Todolist.Api.Data.Projections;
using Todolist.Api.Data.Repositories;

namespace Todolist.Api.Extensions;

public static class ApplicationExtensions
{
    public static ContainerBuilder RegisterUseCases(this ContainerBuilder builder)
    {
        builder.Register(_ => DateTimeZoneProviders.Tzdb).As<IDateTimeZoneProvider>();
        builder.Register(c => SystemClock.Instance).As<IClock>();

        return builder;
    }

    public static ContainerBuilder RegisterPersistence(this ContainerBuilder builder)
    {
        builder.Register(c =>
        {
            var connectionString = c.Resolve<IConfiguration>().GetConnectionString("Database");

            return new TodoRepository(connectionString);
        }).As<Data.Repositories.Interfaces.TodoRepository>();

        builder
            .Register(
                c => new GetTodoListsBuilder(c.Resolve<IConfiguration>().GetConnectionString("Database")))
            .As<Todolist.Api.Data.Projections.Interfaces.GetTodoListsBuilder>();

        return builder;
    }
}