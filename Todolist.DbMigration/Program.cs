using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Todolist.DbMigration.Migrations;

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(Environment.GetEnvironmentVariable("ConnectionStrings__Database"))
        .ScanIn(typeof(InitTodoTables).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);
using (var scope = serviceProvider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}