using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data
{
    public class HierarchyDbContext : DbContext
    {
        public HierarchyDbContext(DbContextOptions<HierarchyDbContext> options)
        {
            
        }
    }
}
