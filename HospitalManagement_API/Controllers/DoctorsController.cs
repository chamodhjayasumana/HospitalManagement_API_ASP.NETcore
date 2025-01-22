using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_API.Controllers
{
    public class DoctorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
