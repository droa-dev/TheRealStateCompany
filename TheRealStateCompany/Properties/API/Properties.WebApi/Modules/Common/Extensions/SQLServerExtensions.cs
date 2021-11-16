namespace Properties.WebApi.Modules.Common.Extensions
{
    using Application.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.FeatureManagement;
    using Properties.Domain.Factories;
    using Properties.Domain.Repositories;
    using Properties.Infrastructure.DataAccess.DataProviders.SQLServer;
    using Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories;
    using Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes;
    using Properties.Infrastructure.DataAccess.Factories;
    using Properties.WebApi.Modules.Common.Features;

    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class SQLServerExtensions
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddSQLServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IFeatureManager featureManager = services
                .BuildServiceProvider()
                .GetRequiredService<IFeatureManager>();

            bool isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.SQLServer))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (isEnabled)
            {
                services.AddDbContext<RealStateDbContext>(
                    options => options.UseSqlServer(
                        configuration.GetValue<string>("PersistenceModule:DefaultConnection")));
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                services.AddScoped<IPropertyRepository, PropertyRepository>();
                services.AddScoped<IOwnerRepository, OwnerRepository>();
                services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
                services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
                services.AddScoped<ICountryStatesRepository, CountryStatesRepository>();
            }
            else
            {
                services.AddSingleton<RealStateDbContextFake, RealStateDbContextFake>();
                services.AddScoped<IUnitOfWork, UnitOfWorkFake>();

                services.AddScoped<IPropertyRepository, PropertyRepositoryFake>();
                services.AddScoped<IOwnerRepository, OwnerRepositoryFake>();
                services.AddScoped<IPropertyTraceRepository, PropertyTraceRepositoryFake>();
                services.AddScoped<IPropertyImageRepository, PropertyImageRepositoryFake>();
                services.AddScoped<ICountryStatesRepository, CountryStatesRepositoryFake>();
            }

            services.AddScoped<IPropertyFactory, EntityFactory>();
            services.AddScoped<IOwnerFactory, EntityFactory>();
            services.AddScoped<IPropertyTraceFactory, EntityFactory>();

            return services;
        }
    }
}
