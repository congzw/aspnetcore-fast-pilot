using Demo.Web.Libs.Domain;
using Microsoft.EntityFrameworkCore;

namespace Demo.Web.Libs.Data
{
    public class LinksDbContext : DbContext
    {
        public LinksDbContext (DbContextOptions<LinksDbContext> options): base(options)
        {
        }

        public DbSet<LinkItem> LinkItems { get; set; }
    }
}
