using System.Globalization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Todolist.Api.Extensions;

   public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            Dictionary<string, OpenApiInfo> versions,
            Func<SwaggerGenOptions, SwaggerGenOptions> mapOptions,
            string? xmlCommentsFilePath = null) => services.AddSwaggerGen(
            options =>
            {
                options.CustomSchemaIds(type => type.FullName);
                options.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                if (xmlCommentsFilePath != null)
                {
                    options.IncludeXmlComments(xmlCommentsFilePath);
                }

                foreach (var version in versions)
                {
                    options.SwaggerDoc(version.Key, version.Value);
                }

                mapOptions(options);
            });

        public static IApplicationBuilder UseVersionedSwagger(
            this IApplicationBuilder app,
            string[] versions,
            bool serializeAsV2 = false,
            bool collapseAllByDefault = false)
        {
            app.UseSwagger(
                options =>
                {
                    options.SerializeAsV2 = serializeAsV2;
                    options.RouteTemplate = "/api-docs/swagger/{documentname}/swagger.json";
                });

            app.UseSwaggerUI(
                options =>
                {
                    foreach (var version in versions)
                    {
                        options.SwaggerEndpoint(
                            $"/api-docs/swagger/{version.ToLower(CultureInfo.CurrentCulture)}/swagger.json",
                            version.ToUpper(CultureInfo.CurrentCulture));

                        options.DocExpansion(collapseAllByDefault ? DocExpansion.None : DocExpansion.List);
                        options.EnableDeepLinking();
                    }

                    options.RoutePrefix = "api-docs";
                });

            return app;
        }
    }