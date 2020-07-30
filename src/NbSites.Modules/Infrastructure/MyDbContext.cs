using Microsoft.EntityFrameworkCore;
using NbSites.Modules.Demos;

namespace NbSites.Infrastructure
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options): base(options)
        {
        }

        public DbSet<LinkItem> LinkItems { get; set; }
    }
}
