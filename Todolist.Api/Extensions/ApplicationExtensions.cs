using Autofac;
using NodaTime;

namespace Todolist.Api.Extensions;

public static class ApplicationExtensions
{
    public static ContainerBuilder RegisterUseCases(this ContainerBuilder builder)
    {
        builder.Register(c => SystemClock.Instance).As<IClock>();

        return builder;
    }
}