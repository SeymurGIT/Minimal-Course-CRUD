using Microsoft.AspNetCore.Mvc;

namespace MinimalCourseCRUD.Controllers
{
    public class SalamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
