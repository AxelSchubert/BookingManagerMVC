using BookingManagerMVC.Models;

namespace BookingManagerMVC.Services
{
    public class AdminService
    {
        private readonly HttpClient _client;

        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("BookingApi");
        }
        public async Task<List<T>> GetAsync<T>(string apiEndpoint)
        {
            return await _client.GetFromJsonAsync<List<T>>($"api/{apiEndpoint}");
        }
        public async Task<bool> DeleteAsync(string apiEndpoint, int id) 
        { 
            var response = await _client.DeleteAsync($"api/{apiEndpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditAsync<T>(string apiEndpoint, int id, T updatedItem)
        {
            var response = await _client.PutAsJsonAsync<T>($"api/{apiEndpoint}/{id}", updatedItem);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> CreateAsync<T>(string apiEndpoint, T newItem)
        {
            var response = await _client.PostAsJsonAsync<T>($"api/{apiEndpoint}", newItem);

            return response.IsSuccessStatusCode;
        }
    }
}
