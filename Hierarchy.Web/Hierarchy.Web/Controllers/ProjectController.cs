using Microsoft.AspNetCore.Mvc;

namespace Hierarchy.Web.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
