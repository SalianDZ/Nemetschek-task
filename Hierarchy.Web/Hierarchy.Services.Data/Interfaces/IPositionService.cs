using Hierarchy.Web.Models.Position;

namespace Hierarchy.Services.Data.Interfaces
{
	public interface IPositionService
	{
		Task<IEnumerable<PositionListViewModel>> GetAllPositionsAsync();
		Task<PositionDetailViewModel> GetPositionDetailsAsync(Guid positionId);
		Task AddPositionAsync(PositionFormViewModel positionViewModel);

		Task<bool> DoesPositionExistAsync(string positionName);
	}
}
