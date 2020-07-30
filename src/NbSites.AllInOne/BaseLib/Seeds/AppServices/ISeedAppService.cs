using Common;

namespace NbSites.BaseLib.Seeds.AppServices
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
