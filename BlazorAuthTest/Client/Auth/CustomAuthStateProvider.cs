using BlazorAuthTest.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAuthTest.Client.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(claimsPrincipal);
        }

        public void SetAuthInfo(UserInfo userInfo)
        {
            var identity = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, $"{userInfo.FirstName} {userInfo.LastName}"),
                new Claim("UserId", userInfo.ToString())
            }, "AuthCookie");

            claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void ClearAuthInfo()
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
