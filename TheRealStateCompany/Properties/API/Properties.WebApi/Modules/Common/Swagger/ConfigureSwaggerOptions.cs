namespace Properties.WebApi.Modules.Common.Swagger
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    ///     Configures the Swagger generation options.
    /// </summary>
    /// <remarks>
    ///     This allows API versioning to define a Swagger document per API version after the
    ///     <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
    /// </remarks>
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private const string UriString =
            "https://github.com/droa-dev/TheRealStateCompany/master/README.md";

        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
        /// </summary>
        /// <param name="provider">
        ///     The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
        ///     documents.
        /// </param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this._provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (ApiVersionDescription description in this._provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            OpenApiInfo info = new OpenApiInfo
            {
                Title = "The Real State Company API",
                Version = description.ApiVersion.ToString(),
                Description = "Rest API, using hexagonal architecture, Domain-Driven Design (DDD).",
                Contact = new OpenApiContact { Name = "Diego Roa", Email = "godierle@gmail.com" },
                //TermsOfService = new Uri(UriString),
                //License = new OpenApiLicense
                //{
                //    Name = "Apache License",
                //    Url = new Uri(
                //        UriString)
                //}
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
