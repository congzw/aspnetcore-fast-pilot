using Common;

namespace NbSites.Web.Libs.Services
{
    public interface ISeedAppService
    {
        MessageResult ResetDb(ResetDbArgs args);
    }

    public class ResetDbArgs
    {
        public bool ClearIfExist { get; set; }
    }
}
