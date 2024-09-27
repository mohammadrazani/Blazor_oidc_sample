namespace Client.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server.Circuits;

    public class UserService
    {
        private ClaimsPrincipal currentUser = new(new ClaimsIdentity());

        public ClaimsPrincipal GetUser()
        {
            return currentUser;
        }

        public string? GetName()
        {
            if (currentUser == null)
            {
                return null;
            }

            if (currentUser.Identity == null || string.IsNullOrEmpty(currentUser.Identity.Name))
            {
                var name = currentUser.Claims?.FirstOrDefault(x => x.Type == "name");

                return name?.Value;
            }

            return currentUser.Identity.Name;
        }

        internal void SetUser(ClaimsPrincipal user)
        {
            if (currentUser != user)
            {
                currentUser = user;
            }
        }
    }

}
