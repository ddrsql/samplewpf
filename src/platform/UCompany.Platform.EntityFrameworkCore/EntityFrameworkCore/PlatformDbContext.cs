using Microsoft.EntityFrameworkCore;
using UCompany.Platform.TodoItems.Aggregates;
using Abp.EntityFrameworkCore;

namespace UCompany.Platform.EntityFrameworkCore
{
    public class PlatformDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<TodoItem> TodoItem { get; set; }

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options)
            : base(options)
        {
        }
    }
}
