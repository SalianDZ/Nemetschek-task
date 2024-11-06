using Hierarchy.Services.Data;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Department;
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                DepartmentFormViewModel formViewModel = new DepartmentFormViewModel();
                return View(formViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await departmentService.AddDepartmentAsync(model);
                return RedirectToAction("All", "Department");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var department = await departmentService.GetDepartmentDetailsAsync(Guid.Parse(id));

				// No need for additional null checks since the service always returns a valid view model
				return View(department);
			}
            catch (Exception)
            {
				return View("Error");
			}
        }
    }
}
