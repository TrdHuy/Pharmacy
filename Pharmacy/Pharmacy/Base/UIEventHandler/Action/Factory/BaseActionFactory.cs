using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action.Factory
{
    public class BaseActionFactory : AbstractActionFactory
    {
        public override IAction CreateAction(string builderID, string keyID, BaseViewModel viewModel = null, ILogger logger = null)
        {
            IActionBuilder builder = null;
            try
            {
                builder = _builders[builderID];
            }
            catch
            {
                return null;
            }

            if (!builder.Locker.IsLock)
            {
                return builder.CreateMainAction(keyID);
            }
            else
            {
                return builder.CreateAlternativeActionWhenFactoryIsLock(keyID);
            }
        }
    }
}
