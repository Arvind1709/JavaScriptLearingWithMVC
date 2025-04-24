//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Net.Http.Headers;

//namespace JavaScriptLearingWithMVC.Controllers
//{
//    public class ApiService
//    {
//        private readonly HttpClient _httpClient;

//       // private readonly HttpClient _httpClient1;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
//        {
//            _httpClient = httpClient;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        private void AddAuthorizationHeader()
//        {
//            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
//            if (!string.IsNullOrEmpty(token))
//            {
//                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            }
//        }

//        // Generic method to fetch data for any type
//        public async Task<List<T>> GetAllAsync<T>(string endpoint)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.GetAsync(endpoint);

//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadFromJsonAsync<List<T>>();
//            }

//            return new List<T>(); // Return an empty list if the API call fails
//        }

//        // Generic method to fetch a single item by ID
//        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.GetAsync($"{endpoint}/{id}");

//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadFromJsonAsync<T>();
//            }

//            return default; // Return default value (null for reference types) if the API call fails
//        }

//        // Generic method to create a new item
//        public async Task<bool> CreateAsync<T>(string endpoint, T item)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PostAsJsonAsync(endpoint, item);

//            return response.IsSuccessStatusCode;
//        }

//        // Generic method to create a new item
//        public async Task<bool> CreateAsyncHttpContent(string endpoint, HttpContent content)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PostAsync(endpoint, content); // Change to PostAsync

//            return response.IsSuccessStatusCode;
//        }

//        // Generic method to get post record 
//        //public async Task<object> GetPostRecordAsync<T>(string endpoint, T item)
//        //{
//        //    var response = await _httpClient.PostAsJsonAsync(endpoint, item);
//        //    if (response.IsSuccessStatusCode)
//        //    {
//        //        return await response.Content.ReadFromJsonAsync<object>();
//        //    }

//        //    return response.IsSuccessStatusCode;
//        //}

//        public async Task<string> GetPostRecordAsync<T>(string endpoint, T item)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PostAsJsonAsync(endpoint, item);

//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadAsStringAsync(); // Return response as a string
//            }

//            return null; // Return null if request fails
//        }

//        //public async Task<string> GetPostRecordAsync<T>(string endpoint, T item)
//        //{
//        //    AddAuthorizationHeader();
//        //    var response = await _httpClient.PostAsJsonAsync(endpoint, item);

//        //    if (response.IsSuccessStatusCode)
//        //    {
//        //        return await response.Content.ReadAsStringAsync(); // ✅ Return response as a string
//        //    }

//        //    return null; // Return null if request fails
//        //}

//    }


//}

using System.Net.Http.Headers;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Adds the JWT Bearer token to the Authorization header, if available in session.
        /// </summary>
        private void AddAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token) &&
                _httpClient.DefaultRequestHeaders.Authorization?.Parameter != token)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// Fetches all records of the specified type from the given API endpoint.
        /// </summary>
        public async Task<List<T>> GetAllAsync<T>(string endpoint)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync(endpoint).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<T>>().ConfigureAwait(false);
            }

            return new List<T>();
        }

        /// <summary>
        /// Fetches a single record by ID from the specified endpoint.
        /// </summary>
        public async Task<T?> GetByIdAsync<T>(string endpoint, int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{endpoint}/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
            }

            return default;
        }

        /// <summary>
        /// Sends a POST request with the specified item to create a new record.
        /// </summary>
        public async Task<bool> CreateAsync<T>(string endpoint, T item)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(endpoint, item).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a POST request using raw HttpContent (for file uploads or custom formats).
        /// </summary>
        public async Task<bool> CreateAsyncHttpContent(string endpoint, HttpContent content)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsync(endpoint, content).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a POST request and returns the response as a string.
        /// </summary>
        public async Task<string?> GetPostRecordAsync<T>(string endpoint, T item)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync(endpoint, item).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return null;
        }
    }
}

