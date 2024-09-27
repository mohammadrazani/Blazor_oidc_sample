using Client.Components;
using Client.Middlewares;

namespace Client.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureApp(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseMiddleware<UserServiceMiddleware>();

            app.MapRazorComponents<App>()
                .RequireAuthorization()
                .AddInteractiveServerRenderMode();
        }
    }
}
