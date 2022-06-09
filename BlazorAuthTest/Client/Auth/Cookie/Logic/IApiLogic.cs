using BlazorAuthTest.Shared;

namespace BlazorAuthTest.Client.Auth.Cookie.Logic
{
    public interface IApiLogic
    {
        Task<string> LoginAsync(LoginRequest login);
        Task<(string Message, UserInfo? UserProfile)> UserInfoAsync();
        Task<string> LogoutAsync();
    }
}
