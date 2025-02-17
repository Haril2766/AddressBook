using Microsoft.AspNetCore.Mvc;

namespace Address_Book.Controllers
{
    public class HelperTagController : Controller
    {
        public IActionResult Helper()
        {
            return View();
        }
    }
}
