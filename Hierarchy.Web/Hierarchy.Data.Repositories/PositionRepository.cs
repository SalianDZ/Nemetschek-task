using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly HierarchyDbContext context;
        public PositionRepository(HierarchyDbContext context)
        {
            this.context = context;
        }

        public async Task AddPositionAsync(Position position)
        {
            await context.Positions.AddAsync(position);
            await context.SaveChangesAsync();
        }

        public async Task DeletePositionAsync(Guid id)
        {
            var position = await GetPositionByIdAsync(id);
            if (position != null)
            {
                context.Positions.Remove(position);
                await context.SaveChangesAsync();
            }
        }

        public Task<bool> DoesPositionExistAsync(string name)
        {
            return context.Positions.AnyAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await context.Positions.ToListAsync();
        }

		public async Task<IEnumerable<Position>> GetAllPositionsWithEmployeesAsync()
		{
			return await context.Positions
				.Include(p => p.Employees)
				.ToListAsync();
		}

		public async Task<Position> GetPositionByIdAsync(Guid id)
        {
            return await context.Positions.FindAsync(id);
        }

		public async Task<Position> GetPositionWithEmployeesByIdAsync(Guid positionId)
		{
			return await context.Positions
				.Include(p => p.Employees)
				.ThenInclude(e => e.Department)
				.FirstOrDefaultAsync(p => p.Id == positionId);
		}

		public async Task UpdatePositionAsync(Position position)
        {
            context.Positions.Update(position);
            await context.SaveChangesAsync();
        }
    }
}
