using Hierarchy.Data.Models;
using Hierarchy.Web.Models.Position;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> GetPositionByIdAsync(Guid id);
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task AddPositionAsync(Position position);
        Task UpdatePositionAsync(PositionFormViewModel model, Guid id);
        Task DeletePositionAsync(Guid id);
		Task<IEnumerable<Position>> GetAllPositionsWithEmployeesAsync();
		Task<Position> GetPositionWithEmployeesByIdAsync(Guid positionId);
        Task<bool> DoesPositionExistAsync(string name);
	}
}
