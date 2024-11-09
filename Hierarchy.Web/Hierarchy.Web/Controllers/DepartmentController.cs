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
            try
            {
				var departments = await departmentService.GetAllDepartmentsAsync();
				return View(departments);
			}
            catch (Exception)
            {
				return NotFound("An error occured during the process of connecting to the database!");
			}
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
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await departmentService.DoesDepartmentExistAsync(model.Name))
            {
                return NotFound("There is already a department with the same name!");
            }

            try
            {
                await departmentService.AddDepartmentAsync(model);
                return RedirectToAction("All", "Department");
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid checkedId))
                {
                    return NotFound("There is no such a department!");
                }

                var department = await departmentService.GetDepartmentDetailsAsync(Guid.Parse(id));

				// No need for additional null checks since the service always returns a valid view model
                //We should the best way for here!!!!!!
				return View(department);
			}
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            var department = await departmentService.GetDepartmentDetailsAsync(correctId);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                await departmentService.DeleteDepartmentAsync(correctId);
                return RedirectToAction("All", "Department");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Delete", "Department");
            }
        }
    }
}
