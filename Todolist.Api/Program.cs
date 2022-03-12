using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;
using Todolist.Api.Exceptions;
using Todolist.Api.Extensions;

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
        .RegisterUseCases().RegisterPersistence());

builder.Services.AddControllers()
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

await app.RunAsync();