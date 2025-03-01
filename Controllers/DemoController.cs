using Microsoft.AspNetCore.Mvc;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        // Success message example (AJAX)
        [HttpPost]
        public JsonResult ShowSuccess()
        {
            return Json(new { status = "success", message = "Data saved successfully!" });
        }

        // Error message example (AJAX)
        [HttpPost]
        public JsonResult ShowError()
        {
            return Json(new { status = "error", message = "An error occurred while processing your request." });
        }

        // Simulating a method that throws an exception
        [HttpPost]
        public JsonResult TotalSale()
        {
            try
            {
                int x = 10;
                int y = 0;
                int result = x / y; // This will throw DivideByZeroException

                return Json(new { status = "success", message = "Total sales calculated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Error: " + ex.Message });
            }
        }
    }
}
