using Microsoft.AspNetCore.Mvc;

namespace Address_Book.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Student()
        {
            return View();
        }
    }
}
