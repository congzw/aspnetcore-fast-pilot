using Microsoft.EntityFrameworkCore;
using NbSites.Web.Libs.Domain;

namespace NbSites.Web.Libs.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options): base(options)
        {
        }

        public DbSet<LinkItem> LinkItems { get; set; }
    }
}
