using Client.Services;
using Client.Handlers;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorComponents()
                .AddInteractiveServerComponents();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:7039";
                    options.ClientId = "client";
                    options.MapInboundClaims = false;
                    options.SaveTokens = true;
                    options.DisableTelemetry = true;
                    options.SkipUnrecognizedRequests = true;
                });

            services.AddAuthorization();

            services.AddCascadingAuthenticationState();

            services.AddScoped<UserService>();
            services.TryAddEnumerable(
                ServiceDescriptor.Scoped<CircuitHandler, UserCircuitHandler>());
        }
    }
}
