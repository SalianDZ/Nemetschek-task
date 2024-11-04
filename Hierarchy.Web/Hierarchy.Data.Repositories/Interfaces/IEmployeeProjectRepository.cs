using Hierarchy.Data.Models;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        //Task AssignEmployeeToProjectAsync(EmployeeProject employeeProject);
        //Task RemoveEmployeeFromProjectAsync(Guid employeeId, Guid projectId);
        Task<IEnumerable<EmployeeProject>> GetEmployeeProjectsByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<EmployeeProject>> GetEmployeeProjectsByProjectIdAsync(Guid projectId);
    }
}
