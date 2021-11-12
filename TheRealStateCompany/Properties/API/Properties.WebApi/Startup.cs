using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Properties.WebApi.Modules.Common.Extensions;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.Modules.Common.Swagger;

namespace Properties.WebApi
{
    public class Startup
    {
        /// <summary>
        ///     Startup constructor.
        /// </summary>
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddFeatureFlags(this.Configuration)
                .AddInvalidRequestLogging()
                .AddSQLServer(this.Configuration)
                .AddHealthChecks(this.Configuration)
                .AddAuthentication(this.Configuration)
                .AddVersioning()
                .AddSwagger()
                .AddUseCases()
                .AddCustomControllers()
                .AddCustomCors()
                .AddProxy()
                .AddCustomDataProtection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/api/V1/CustomError")
            //        .UseHsts();
            //}

            app
                .UseProxy(this.Configuration)
                .UseHealthChecks()
                .UseCustomCors()
                .UseCustomHttpMetrics()
                .UseRouting()
                .UseVersionedSwagger(provider, this.Configuration, env)
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapMetrics();
                });
        }
    }
}
