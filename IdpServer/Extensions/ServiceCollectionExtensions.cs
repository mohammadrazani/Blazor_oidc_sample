using Duende.IdentityServer.Models;
using IdpServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdpServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            services.AddIdentityServer()
                .AddInMemoryClients([
                    new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        RedirectUris = { "https://localhost:7056/signin-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:7056/signout-callback-oidc" },
                        FrontChannelLogoutUri = "https://localhost:7056/signout-oidc",
                        AllowedScopes = { "openid", "profile", "email", "phone" }
                    }
                ])
                .AddInMemoryIdentityResources([
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResources.Phone(),
                ])
                .AddAspNetIdentity<IdentityUser>();

            services.AddLogging(options =>
            {
                options.AddFilter("Duende", LogLevel.Debug);
            });
        }
    }
}
