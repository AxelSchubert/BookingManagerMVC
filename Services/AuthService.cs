using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookingManagerMVC.Models;
using BookingManagerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

                var claims = jwtObject.Claims.ToList();

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var httpContext = _httpContextAccessor.HttpContext;

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = jwtObject.ValidTo
                });

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

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Response.Cookies.Delete("jwtToken");
        }

    }
}
