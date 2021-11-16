namespace Properties.WebApi.Modules.Common.Extensions
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.FeatureManagement;
    using Microsoft.IdentityModel.Tokens;
    using Properties.WebApi.Modules.Common.Features;
    using Properties.WebApi.Modules.Common.JWT;
    using System;
    using System.Text;

    /// <summary>
    ///     Authentication Extensions.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        ///     Add Authentication Extensions.
        /// </summary>
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IFeatureManager featureManager = services
                .BuildServiceProvider()
                .GetRequiredService<IFeatureManager>();

            bool isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.Authentication))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            services.Configure<JwtConfigs>(configuration.GetSection("AuthenticationModule"));
            services.AddScoped<IAuthTokenHandler, AuthTokenHandler>();

            if (isEnabled)
            {
                //services.AddScoped<IUserService, ExternalUserService>();

                var TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration.GetValue<string>("AuthenticationModule:ValidIssuer"),
                    ValidAudience = configuration.GetValue<string>("AuthenticationModule:ValidAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AuthenticationModule:SingingKey"))),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };

                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.TokenValidationParameters = TokenValidationParameters;
                    });

                services.AddAuthorization();
            }
            else
            {
                //services.AddScoped<IUserService, TestUserService>();

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = "Test";
                    x.DefaultChallengeScheme = "Test";
                }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                    "Test", options => { });
            }

            return services;
        }
    }
}
