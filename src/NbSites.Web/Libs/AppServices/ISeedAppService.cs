using Common;

namespace NbSites.Web.Libs.AppServices
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
