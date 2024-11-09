using Hierarchy.Data.Models;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> GetPositionByIdAsync(Guid id);
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task AddPositionAsync(Position position);
        Task UpdatePositionAsync(Position position);
        Task DeletePositionAsync(Guid id);
		Task<IEnumerable<Position>> GetAllPositionsWithEmployeesAsync();
		Task<Position> GetPositionWithEmployeesByIdAsync(Guid positionId);
        Task<bool> DoesPositionExistAsync(string name);
	}
}
