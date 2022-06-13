using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using Serilog.Formatting.Json;
using Todolist.Api.Exceptions;
using Todolist.Api.Extensions;

Log.Information("CreateBuilder");
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog((context, cfg) => cfg.GetConfiguration(context.Configuration, new JsonFormatter(renderMessage: true)))
    .ConfigureServices(
        (_, services) =>
        {
            services.AddPersistence();
            services.AddMemoryCache();
            services.AddOpenApi();
            services.AddErrorOptions(builder.Configuration);
            services.AddVersioning(1, 0);
            services.AddRouting(options => options.LowercaseUrls = true);
        })
    .ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder
        .RegisterUseCases()
        .RegisterPersistence());

Log.Information("Builder services start");
builder.Services
    .AddControllers()
    .AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        })
    .AddErrorFilterHandling();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseVersionedOpenApi();
app.UseLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("Application Start");
await app.RunAsync();