using BlazorAuthTest.Shared;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorAuthTest.Client.Auth.Cookie.Logic
{
    public class ApiLogic : IApiLogic
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiLogic(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> LoginAsync(LoginRequest login)
        {
			var client = _httpClientFactory.CreateClient("API");
			string payload = JsonSerializer.Serialize(login);
			var content = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("/Auth/login", content);
			if (response.IsSuccessStatusCode)
			{
				return "Success";
			}
			else
			{
				return "failed";
			}
		}

        public async Task<(string Message, UserInfo? UserProfile)> UserInfoAsync()
        {
			var client = _httpClientFactory.CreateClient("API");
			var response = await client.GetAsync("/Auth/user-profile");

			try
            {
				return ("Success", await response.Content.ReadFromJsonAsync<UserInfo>());
            } catch 
            {
				return ("Failed", null);
            }

			//if (response.IsSuccessStatusCode)
			//{
			//	return ("Success", await response.Content.ReadFromJsonAsync<UserInfo>());
			//}
			//else
			//{
			//	if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			//	{
			//		return ("Unauthorized", null);
			//	}
			//	else
			//	{
			//		return ("Failed", null);
			//	}
			//}
		}

		public async Task<string> LogoutAsync()
		{
			var client = _httpClientFactory.CreateClient("API");
			var response = await client.PostAsync("/Auth/logout", null);
			if (response.IsSuccessStatusCode)
			{
				return "Success";
			}
			return "Failed";
		}
	}
}
