using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
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

        #region OnKeyDestroy field
        public void OnKeyDestroy(string windowTag, string keyFeature)
        {
            IAction action;
            try
            {
                action = _actionExecuteHelper.GetActionInCache(windowTag, keyFeature);
            }
            catch
            {
                action = null;
            }

            if (action != null)
            {
                IDestroyable destroyableAction = action as IDestroyable;
                if (destroyableAction != null)
                {
                    destroyableAction.OnDestroy();
                }
            }
        }
        #endregion

        #region Onkey and execute action field
        public void OnKey(string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature);
            if (action != null)
            {
                ExetcuteAction(dataTransfer, action);
            }
        }

        public void OnKey(string windowTag, string keyFeature, object dataTransfer, BuilderLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status);
            if (action != null)
            {
                ExetcuteAction(dataTransfer, action);
            }
        }

        public void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature, viewModel, logger);
            if (action != null)
            {
                ExetcuteAction(dataTransfer, action);
            }
        }

        public void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer, BuilderLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status, viewModel, logger);
            if (action != null)
            {
                ExetcuteAction(dataTransfer, action);
            }
        }

        private void ExetcuteAction(object dataTransfer, IAction action)
        {
            var status = _actionExecuteHelper.ExecuteAction(action, dataTransfer);

            if(status == ExecuteStatus.ExistedExecuter)
            {
                //Implement action when an executer already exist in cache
            }
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
            , ILogger logger = null
            , bool isDestroyableCommandExecuter = false)
        {
            return GetAction(keytag, windowTag, viewModel, logger, isDestroyableCommandExecuter);
        }

        private IAction GetKeyActionAndLockFactory(string windowTag
            , string keytag
            , bool isLock = false
            , BuilderStatus status = BuilderStatus.Default
            , BaseViewModel viewModel = null
            , ILogger logger = null
            , bool isDestroyableCommandExecuter = false)
        {
            var action = GetAction(keytag, windowTag, viewModel, logger, isDestroyableCommandExecuter);
            _commandExecuterFactory.LockBuilder(windowTag, isLock, status);

            return action;
        }

        private IAction GetAction(string keyTag
            , string builderID
            , BaseViewModel viewModel = null
            , ILogger logger = null
            , bool isDestroyableCommandExecuter = false)
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
                _commandExecuterFactory.TurnDestroyableActionBuilder(isDestroyableCommandExecuter);
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
