using Microsoft.AspNetCore.Mvc;

namespace CorePlatform.Api.Controllers
{
    public class AppointmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
