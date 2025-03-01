using JavaScriptLearingWithMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;



namespace JavaScriptLearingWithMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;
        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var jsonResponse = await _apiService.GetPostRecordAsync("api/Auth/login", model);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var obj = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                if (obj != null && !string.IsNullOrEmpty(obj.Token))
                {
                    HttpContext.Session.SetString("JwtToken", obj.Token); // Store token in session
                    return RedirectToAction("Index", "Users");
                }
            }

            ModelState.AddModelError("", "Invalid login credentials.");
            return View(model); // Stay on login page if authentication fails
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            ModelState.Remove(model.ConfirmPassword);
            if (ModelState.IsValid)
            {
                var newUser = new UserModel
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hash password
                    Role = "Customer",
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    DateCreated = DateTime.Now,
                    IsActive = true
                };
                // Ensure correct serialization
                var json = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var jsonResponse = await _apiService.CreateAsyncHttpContent("api/Users/create", content);
                if(jsonResponse)
                {
                    return RedirectToAction("Login"); // Redirect to login page
                }
            }
            ModelState.AddModelError("", "User Not Regerter yet.");
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken"); // 🔹 Remove the token from session
            return RedirectToAction("Login", "Auth"); // 🔹 Redirect to login page
        }





        class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
