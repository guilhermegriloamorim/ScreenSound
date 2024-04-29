using Microsoft.Extensions.Http;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class AuthAPI
    {
        private readonly HttpClient _client;
        public AuthAPI(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("API");
        }

        public async Task<AuthResponse> Login(string email, string senha)
        {

            var response = await _client.PostAsJsonAsync("auth/login", new
            {
                email,
                password = senha
            });

            if (response.IsSuccessStatusCode)
            {
                return new AuthResponse { Success = true };
            }
            else
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();
                return new AuthResponse { Success = false, Errors = errors };
            }
        }
    }
}
