using Hierarchy.Services.Data;
using Hierarchy.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hierarchy.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }


        public async Task<IActionResult> All()
        {
            var departments = await departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }
    }
}
