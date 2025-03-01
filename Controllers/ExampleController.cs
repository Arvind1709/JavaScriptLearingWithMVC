using Microsoft.AspNetCore.Mvc;

namespace JavaScriptLearingWithMVC.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        // Success Message Example
        public ActionResult ShowSuccess()
        {
            TempData["Message"] = "Data saved successfully!";
            TempData["MessageType"] = "success"; // Can be success, error, warning, info
            return RedirectToAction("Index");
        }

        // Error Message Example
        public ActionResult ShowError()
        {
            TempData["Message"] = "An error occurred while processing your request.";
            TempData["MessageType"] = "error"; // Can be success, error, warning, info
            return RedirectToAction("Index");
        }

        // Simulating a method that throws an exception
        public ActionResult TotalSale()
        {
            try
            {
                int x = 10;
                int y = 0;
                int result = x / y; // This will throw DivideByZeroException

                TempData["Message"] = "Total sales calculated successfully!";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error: " + ex.Message; // Capture exception message
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index");
        }
    }
}
