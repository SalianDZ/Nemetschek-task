﻿using Hierarchy.Data.Models;
using Hierarchy.Services.Data;
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
            try
            {
                var employees = await employeeService.GetAllEmployeesAsync();
                return View(employees);
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
                EmployeeFormViewModel model = new();
                ViewBag.GenderOptions = await employeeService.GetGenderOptions();
                model.Departments = await departmentService.GetAllDepartmentsForSelectAsync();
                model.Positions = await positionService.GetAllPositionsForSelectAsync();
                model.Supervisors = await employeeService.GetAllSupervisorsForSelectAsync();
                return View(model);
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
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

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid checkedId))
                {
                    return NotFound("There is no such a department!");
                }

                var department = await employeeService.GetEmployeeDetailsAsync(checkedId);

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

            var employee = await employeeService.GetEmployeeDetailsAsync(correctId);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
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
                await employeeService.DeleteEmployeeAsync(correctId);
                return RedirectToAction("All", "Employee");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Delete", "Employee");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                ViewBag.GenderOptions = await employeeService.GetGenderOptions();
                EmployeeFormViewModel model = await employeeService.GetEmployeeForEditAsync(correctId);
                model.Departments = await departmentService.GetAllDepartmentsForSelectAsync();
                model.Positions = await positionService.GetAllPositionsForSelectAsync();
                model.Supervisors = await employeeService.GetAllSupervisorsForSelectAsync();
                return View(model);
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeFormViewModel model, string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                await employeeService.UpdateEmployeeAsync(model, correctId);
                return RedirectToAction("All", "Employee");
            }

            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }
    }
}
