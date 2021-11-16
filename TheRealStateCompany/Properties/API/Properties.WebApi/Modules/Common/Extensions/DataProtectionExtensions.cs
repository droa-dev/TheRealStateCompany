namespace Properties.WebApi.Modules.Common.Extensions
{
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;

    /// <summary>
    ///     Data Protection Extensions.
    /// </summary>
    public static class DataProtectionExtensions
    {
        /// <summary>
        ///     Add Data Protection.
        /// </summary>
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services)
        {
            services.AddDataProtection()
                .SetApplicationName("properties-api")
                .PersistKeysToFileSystem(new DirectoryInfo(@"./"));

            return services;
        }
    }
}
