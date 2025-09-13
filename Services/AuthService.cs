using System.IdentityModel.Tokens.Jwt;
using BookingManagerMVC.Models;
using BookingManagerMVC.Models.ViewModels;

namespace BookingManagerMVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _client = clientFactory.CreateClient("BookingApi");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(LoginVM loginAdmin)
        {
            var response = await _client.PostAsJsonAsync("api/auth/login", loginAdmin);
            if (response.IsSuccessStatusCode)
            {
               
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                var jwt = tokenResponse.Token;

                var handler = new JwtSecurityTokenHandler();
                var jwtObject = handler.ReadJwtToken(jwt);

                var httpContext = _httpContextAccessor.HttpContext;

                httpContext.Response.Cookies.Append("jwtToken", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = jwtObject.ValidTo
                });

                return true;
            }
            return false;
        }


    }
}
