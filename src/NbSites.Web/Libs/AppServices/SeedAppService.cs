using Common;
using NbSites.Web.Libs.Data;

namespace NbSites.Web.Libs.AppServices
{
    public class SeedAppService : ISeedAppService
    {
        private readonly MyDbContext _dbContext;

        public SeedAppService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MessageResult ResetDb(ResetDbArgs args)
        {
            if (args.ClearIfExist)
            {
                _dbContext.Database.EnsureDeleted();
            }
            _dbContext.Database.EnsureCreated();
            
            //using (var tx = _dbContext.Database.BeginTransaction())
            //{
            //    var linkItems = _dbContext.LinkItems.ToList();
            //    if (linkItems.Count == 0)
            //    {
            //        linkItems.Add(new LinkItem { Title = "Link", Href = "#", Description = "连接" });
            //        _dbContext.SaveChanges();
            //    }
            //    tx.Commit();
            //}
            return MessageResult.Create(true, "ResetDb Complete");
        }

    }
}