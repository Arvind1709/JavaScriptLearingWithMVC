using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Generic method to fetch data for any type
        public async Task<List<T>> GetAllAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<T>>();
            }

            return new List<T>(); // Return an empty list if the API call fails
        }

        // Generic method to fetch a single item by ID
        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default; // Return default value (null for reference types) if the API call fails
        }

        // Generic method to create a new item
        public async Task<bool> CreateAsync<T>(string endpoint, T item)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, item);

            return response.IsSuccessStatusCode;
        }
    }


}
