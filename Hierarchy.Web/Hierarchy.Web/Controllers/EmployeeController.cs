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
    }
}
