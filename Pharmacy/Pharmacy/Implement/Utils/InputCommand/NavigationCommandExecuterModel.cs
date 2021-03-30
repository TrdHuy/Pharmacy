using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Action.Executer;
using Pharmacy.Implement.UIEventHandler.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.InputCommand
{
    public class NavigationCommandExecuterModel : CommandExecuterModel
    {
        private INavigationCommandExecuter _commandExecuterCache;
        protected override ICommandExecuter CommandExecuterCache { get => _commandExecuterCache; set => _commandExecuterCache = value as INavigationCommandExecuter; }
        public NavigationCommandExecuterModel(Func<object, ICommandExecuter> hpssAction) : base(hpssAction)
        {
        }

        protected override void ExetcuteAction(object dataTransfer)
        {
            if (CommandExecuterCache == null)
            {
                return;
            }

            if (ActionExecuteHelper.Status == HelperStatus.RemainSomeExecutingActions
                && ((INavigationCommandExecuter)CommandExecuterCache).PreviewGoToNewSource())
            {
                //if the navigation action is going to navigating to new source, cancel all current processing action
                ActionExecuteHelper.CancelAllAction();

                base.ExetcuteAction(dataTransfer);
            }
            else
            {
                base.ExetcuteAction(dataTransfer);
            }

        }
    }
}
