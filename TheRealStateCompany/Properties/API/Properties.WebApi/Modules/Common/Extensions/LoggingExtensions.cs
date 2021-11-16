namespace Properties.WebApi.Modules.Common.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    /// <summary>
    /// Logging Extensions.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Add Invalid Request Logging
        /// </summary>
        /// <param name="services"></param>
        /// <returns>services</returns>
        public static IServiceCollection AddInvalidRequestLogging(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    ILogger<Startup>? logger = actionContext
                        .HttpContext
                        .RequestServices
                        .GetRequiredService<ILogger<Startup>>();

                    List<string> errors = actionContext.ModelState
                        .Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    string jsonModelState = JsonSerializer.Serialize(errors);
                    logger.LogWarning("Invalid request.", jsonModelState);

                    ValidationProblemDetails problemDetails = new ValidationProblemDetails(actionContext.ModelState);
                    return new BadRequestObjectResult(problemDetails);
                };
            });

            return services;
        }
    }
}
