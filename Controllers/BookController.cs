using JavaScriptLearingWithMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;
        private readonly string _apiUrl = "https://localhost:44338/api/Book"; // Replace with actual API URL

        public BookController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/index");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var books = JsonSerializer.Deserialize<List<BookModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(books);
            }
            return View(new List<BookModel>());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var book = JsonSerializer.Deserialize<BookModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(book);
            }
            return NotFound();
        }


        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<IActionResult> Create(BookModel book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            var json = JsonSerializer.Serialize(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/create", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the book.");
            return View(book);
        }


        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to fetch book details.");
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var book = JsonSerializer.Deserialize<BookModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        // POST: Book/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookModel book)
        {
            if (id != book.Id)
            {
                return BadRequest("The book ID in the URL does not match the book ID in the form.");
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            var json = JsonSerializer.Serialize(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the book.");
            return View(book);
        }


        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var book = JsonSerializer.Deserialize<BookModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(book);
        }


        // POST: Book/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the book. Please try again.");
                return NotFound($"Book with ID {id} not found or could not be deleted.");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
