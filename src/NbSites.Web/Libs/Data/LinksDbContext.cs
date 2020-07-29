using Microsoft.EntityFrameworkCore;
using NbSites.Web.Libs.Domain;

namespace NbSites.Web.Libs.Data
{
    public class LinksDbContext : DbContext
    {
        public LinksDbContext (DbContextOptions<LinksDbContext> options): base(options)
        {
        }

        public DbSet<LinkItem> LinkItems { get; set; }
    }
}
