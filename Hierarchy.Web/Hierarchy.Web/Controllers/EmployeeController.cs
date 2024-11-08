using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Hierarchy.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;
        public EmployeeController(IEmployeeService employeeService,
            IPositionService positionService,
            IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }
        public async Task<IActionResult> All()
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EmployeeFormViewModel model = new();
            ViewBag.GenderOptions = await employeeService.GetGenderOptions();
            model.Departments = await departmentService.GetAllDepartmentsForSelectAsync();
            model.Positions = await positionService.GetAllPositionsForSelectAsync();
            model.Supervisors = await employeeService.GetAllSupervisorsForSelectAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenderOptions = await employeeService.GetGenderOptions();
                model.Departments = await departmentService.GetAllDepartmentsForSelectAsync();
                model.Positions = await positionService.GetAllPositionsForSelectAsync();
                model.Supervisors = await employeeService.GetAllSupervisorsForSelectAsync();
                return View(model);
            }

            if (await employeeService.DoesEmployeeExistByNameAsync(model.Name))
            {
                return NotFound("There is already an employee with the same name!");
            }

            if (model.Gender < 1 || model.Gender > 4)
            { 
                return NotFound("Enter the valid gender!");
            }

            try
            {
                await employeeService.AddEmployeeAsync(model);
                return RedirectToAction("All", "Employee");
            }
            catch (Exception)
            {
                return NotFound("An error occured while saving the data! Try again!");
            }
        }
    }
}
