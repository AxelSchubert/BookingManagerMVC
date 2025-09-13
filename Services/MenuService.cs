using System.Text.Json;
using BookingManagerMVC.Models;

namespace BookingManagerMVC.Services
{
    public class MenuService
    {
        private readonly HttpClient _client;
        public MenuService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("BookingApi");        
        }

        public async Task<List<Course>?> GetMenuAsync()
        {
            return await _client.GetFromJsonAsync<List<Course>>("api/course");
        }
    }
}
