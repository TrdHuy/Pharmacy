using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Action.Executer;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Factory;
using System.Collections.Generic;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class KeyActionListener : IActionListener
    {
        private static KeyActionListener _instance;

        private Pharmacy_CommandExecuterFactory _commandExecuterFactory;
        private ActionExecuteHelper _actionExecuteHelper;

        private KeyActionListener()
        {
            _commandExecuterFactory = Pharmacy_CommandExecuterFactory.Current;
            _actionExecuteHelper = ActionExecuteHelper.Current;
        }

        #region Onkey and execute action field
        public IAction OnKey(string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature);
            return action;
        }

        public IAction OnKey(string windowTag, string keyFeature, object dataTransfer, BuilderLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status);
            return action;
        }

        public IAction OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature, viewModel, logger);
            return action;
        }

        public IAction OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer, BuilderLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status, viewModel, logger);
            return action;
        }

        #endregion

        #region public methods
        public void LockMSW_ActionFactory(bool key, BuilderStatus status)
        {
            _commandExecuterFactory.LockBuilder(WindowTag.WINDOW_TAG_MAIN_SCREEN, key, status);
        }

        public void LockLSW_ActionFactory(bool key, BuilderStatus status)
        {
            _commandExecuterFactory.LockBuilder(WindowTag.WINDOW_TAG_LOGIN_SCREEN, key, status);
        }
        public BuilderStatus GetMSWFactoryStatus()
        {
            return _commandExecuterFactory.Builders[WindowTag.WINDOW_TAG_MAIN_SCREEN].Locker.Status;
        }
        public BuilderStatus GetLSWFactoryStatus()
        {
            return _commandExecuterFactory.Builders[WindowTag.WINDOW_TAG_LOGIN_SCREEN].Locker.Status;
        }

        #endregion

        private IAction GetKeyActionType(string windowTag
            , string keytag
            , BaseViewModel viewModel = null
            , ILogger logger = null)
        {
            return GetAction(keytag, windowTag, viewModel, logger);
        }

        private IAction GetKeyActionAndLockFactory(string windowTag
            , string keytag
            , bool isLock = false
            , BuilderStatus status = BuilderStatus.Default
            , BaseViewModel viewModel = null
            , ILogger logger = null)
        {
            var action = GetAction(keytag, windowTag, viewModel, logger);
            _commandExecuterFactory.LockBuilder(windowTag, isLock, status);

            return action;
        }

        private IAction GetAction(string keyTag
            , string builderID
            , BaseViewModel viewModel = null
            , ILogger logger = null)
        {
            IAction action;
            try
            {
                action = _actionExecuteHelper.GetActionInCache(builderID, keyTag);
            }
            catch
            {
                action = null;
            }

            if (action == null)
            {
                action = _commandExecuterFactory.CreateAction(builderID, keyTag, viewModel, logger);
            }

            return action;
        }

        public static KeyActionListener Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KeyActionListener();
                }
                return _instance;
            }
        }
    }
}
