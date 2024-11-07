using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;
using Hierarchy.Web.Models.Position;

namespace Hierarchy.Services.Data
{
    public class PositionService : IPositionService
	{
		private readonly IPositionRepository positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
			    this.positionRepository = positionRepository;
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
	}
}
