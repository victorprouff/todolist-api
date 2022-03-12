using Autofac;
using NodaTime;
using Todolist.Api.Data.Repositories;

namespace Todolist.Api.Extensions;

public static class ApplicationExtensions
{
    public static ContainerBuilder RegisterUseCases(this ContainerBuilder builder)
    {
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

        return builder;
    }
}