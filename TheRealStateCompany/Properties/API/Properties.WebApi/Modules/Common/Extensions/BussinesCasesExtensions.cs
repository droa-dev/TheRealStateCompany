using Microsoft.Extensions.DependencyInjection;
using Properties.Application.BussinesCases.AddPropertyImage;
using Properties.Application.BussinesCases.ChangePropertyPrice;
using Properties.Application.BussinesCases.CreateOwner;
using Properties.Application.BussinesCases.CreateProperty;
using Properties.Application.BussinesCases.ListProperty;
using Properties.Application.BussinesCases.UpdateProperty;
using Properties.Application.Services;

namespace Properties.WebApi.Modules.Common.Extensions
{
    /// <summary>
    ///     Adds Use Cases classes.
    /// </summary>
    public static class UseCasesExtensions
    {
        /// <summary>
        ///     Adds Bussines Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Notification, Notification>();

            services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();
            services.Decorate<ICreatePropertyUseCase, CreatePropertyValidationUseCase>();

            services.AddScoped<IUpdatePropertyUseCase, UpdatePropertyUseCase>();
            services.Decorate<IUpdatePropertyUseCase, UpdatePropertyValidationUseCase>();

            services.AddScoped<IAddPropertyImageUseCase, AddPropertyImageUseCase>();
            services.Decorate<IAddPropertyImageUseCase, AddPropertyImageValidationUseCase>();            

            services.AddScoped<IChangePropertyPriceUseCase, ChangePropertyPriceUseCase>();
            services.Decorate<IChangePropertyPriceUseCase, ChangePropertyPriceValidationUseCase>();

            services.AddScoped<IListPropertyUseCase, ListPropertyUseCase>();
            services.Decorate<IListPropertyUseCase, ListPropertyValidationUseCase>();

            services.AddScoped<ICreateOwnerUseCase, CreateOwnerUseCase>();
            services.Decorate<ICreateOwnerUseCase, CreateOwnerValidationUseCase>();

            return services;
        }
    }
}
