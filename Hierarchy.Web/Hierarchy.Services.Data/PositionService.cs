using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;
using Hierarchy.Web.Models.Position;

namespace Hierarchy.Services.Data
{
    public class PositionService : IPositionService
	{
		private readonly IPositionRepository positionRepository;
		private readonly IEmployeeRepository employeeRepository;

        public PositionService(IPositionRepository positionRepository, IEmployeeRepository employeeRepository)
        {
            this.positionRepository = positionRepository;
            this.employeeRepository = employeeRepository;
        }

        public async Task AddPositionAsync(PositionFormViewModel positionViewModel)
		{
			var position = new Position
			{
				Name = positionViewModel.Name,
				Rank = positionViewModel.Rank,
				IsSupervisor = positionViewModel.IsSupervisor
			};

			await positionRepository.AddPositionAsync(position);
		}

        public async Task DeletePositionAsync(Guid positionId)
        {
            var hasEmployees = await employeeRepository.HasEmployeesInPositionAsync(positionId);

            if (hasEmployees)
            {
                throw new InvalidOperationException("Cannot delete position with associated employees.");
            }

            await positionRepository.DeletePositionAsync(positionId);
        }

        public async Task<bool> DoesPositionExistAsync(string positionName)
        {
            return await positionRepository.DoesPositionExistAsync(positionName);
        }

        public async Task<IEnumerable<PositionListViewModel>> GetAllPositionsAsync()
		{
			var positions = await positionRepository.GetAllPositionsWithEmployeesAsync();

			return positions.Select(p => new PositionListViewModel
			{
				PositionID = p.Id,
				Name = p.Name,
				EmployeeCount = p.Employees?.Count() ?? 0
			}).ToList();
		}

        public async Task<IEnumerable<PositionSelectViewModel>> GetAllPositionsForSelectAsync()
        {
            IEnumerable<Position> allPositions = await positionRepository.GetAllPositionsAsync();
			return allPositions.Select(p => new PositionSelectViewModel
			{
				Id = p.Id,
				Name = p.Name
			}).ToList();
        }

        public async Task<PositionDetailViewModel> GetPositionDetailsAsync(Guid positionId)
		{
			var position = await positionRepository.GetPositionWithEmployeesByIdAsync(positionId);

			if (position == null)
			{
				return new PositionDetailViewModel
				{
					Name = "Department Not Found",
					Rank = "The requested department does not exist.",
					IsSupervisor = false,
					Employees = new List<EmployeeViewModel>() // Empty list for employees
				};
			}

			return new PositionDetailViewModel
			{
				Name = position.Name,
				Rank = position.Rank.ToString(),
				IsSupervisor = position.IsSupervisor,
				Employees = position.Employees?.Select(e => new EmployeeViewModel
				{
					EmployeeName = e.Name,
					Department = e.Department?.Name 
				}).ToList()
			};
		}

        public async Task<PositionFormViewModel> GetPositionForEditAsync(Guid id)
        {
            Position position = await positionRepository.GetPositionByIdAsync(id);
			PositionFormViewModel model = new();

			model.Name = position.Name;
			model.Rank = position.Rank;
			model.IsSupervisor = position.IsSupervisor;

			return model;
        }

        public async Task UpdateDepartmentAsync(PositionFormViewModel model, Guid id)
        {
			await positionRepository.UpdatePositionAsync(model, id);
        }
    }
}
