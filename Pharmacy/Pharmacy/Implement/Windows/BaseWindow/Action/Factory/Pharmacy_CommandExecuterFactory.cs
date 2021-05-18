using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Action.Factory;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Builder;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory;
using Pharmacy.Implement.Windows.PopupScreenWindow.Action.Factory;
using System;
using System.Collections.Generic;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Factory
{
    internal class Pharmacy_CommandExecuterFactory : BaseCommandExecuterFactory
    {
        private static Logger logger = new Logger("Pharmacy_CommandExecuterFactory");

        private static Pharmacy_CommandExecuterFactory _instance;

        public static Pharmacy_CommandExecuterFactory Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Pharmacy_CommandExecuterFactory();
                }
                return _instance;
            }
        }

        private Pharmacy_CommandExecuterFactory()
        {
            RegisterBuilder(WindowTag.WINDOW_TAG_BASE_WINDOW, new BaseCommandExecuterBuilder());
            RegisterBuilder(WindowTag.WINDOW_TAG_LOGIN_SCREEN, new LSW_CommandExecuterBuilder());
            RegisterBuilder(WindowTag.WINDOW_TAG_MAIN_SCREEN, new MSW_CommandExecuterBuilder());
            RegisterBuilder(WindowTag.WINDOW_TAG_POPUP_SCREEN, new PSW_CommandExecuterBuilder());
        }

        public void LockBuilder(string builderID, bool key, BuilderStatus status)
        {
            try
            {
                if (key)
                    _builders[builderID].LockBuilder(status);
                else
                    _builders[builderID].UnlockBuilder(status);
            }
            catch (Exception e)
            {
                logger.E(e.Message);
                return;
            }
        }

        public override IAction CreateAction(string builderID, string keyID, BaseViewModel viewModel = null, ILogger logger = null)
        {
            IAction action = base.CreateAction(builderID, keyID, viewModel, logger);

            return action;
        }

     
    }
}
