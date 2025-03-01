using JavaScriptLearingWithMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApiService _apiService;

        public UsersController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _apiService.GetAllAsync<UserModel>("api/Users/index");
            return View(users);
        }
       
    }
}
