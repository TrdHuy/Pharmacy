using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Factory;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory;
using System.Collections.Generic;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class KeyActionListener : IActionListener
    {
        private static KeyActionListener _instance;

        /// <summary>
        /// Actions cache for Main Screen window
        /// Currently only support cache for destroyable action
        /// </summary>
        private Dictionary<string, IAction> _msw_DestroyableActions;

        /// <summary>
        /// Action cache for Login Screen Window
        /// Currently only support cache for destroyable action
        /// </summary>
        private Dictionary<string, IAction> _lsw_DestroyableActions;

        private MSW_CommandExecuterFactory _mSW_ActionFactory;
        private LSW_CommandExecuterFactory _lSW_ActionFactory;

        private KeyActionListener()
        {
            _msw_DestroyableActions = new Dictionary<string, IAction>();
            _lsw_DestroyableActions = new Dictionary<string, IAction>();
            _mSW_ActionFactory = new MSW_CommandExecuterFactory();
            _lSW_ActionFactory = new LSW_CommandExecuterFactory();
        }

        public void OnKeyDestroy(string windowTag, string keyFeature)
        {
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    BasedWindowOnKeyDestroy(_lsw_DestroyableActions, keyFeature);
                    break;
                case WindowTag.WINDOW_TAG_MAIN_SCREEN:
                    BasedWindowOnKeyDestroy(_msw_DestroyableActions, keyFeature);
                    break;
            }
        }

        private void BasedWindowOnKeyDestroy(Dictionary<string, IAction> cacheActionsList, string keyFeature)
        {
            IAction action;
            try
            {
                action = cacheActionsList[keyFeature];
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

                cacheActionsList.Remove(keyFeature);
            }
        }

        public void OnKey(string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void OnKey(string windowTag, string keyFeature, object dataTransfer, FactoryLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature, viewModel, logger);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object dataTransfer, FactoryLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Status, viewModel, logger);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void LockMSW_ActionFactory(bool key, FactoryStatus status)
        {
            if (key)
            {
                _mSW_ActionFactory.LockFactory(status);
            }
            else
            {
                _mSW_ActionFactory.UnlockFactory(status);
            }
        }

        public void LockLSW_ActionFactory(bool key, FactoryStatus status)
        {
            if (key)
            {
                _lSW_ActionFactory.LockFactory(status);
            }
            else
            {
                _lSW_ActionFactory.UnlockFactory(status);
            }
        }

        public FactoryStatus GetMSWFactoryStatus()
        {
            return _mSW_ActionFactory.Locker.Status;
        }
        public FactoryStatus GetLSWFactoryStatus()
        {
            return _lSW_ActionFactory.Locker.Status;
        }

        private IAction GetKeyActionType(string windowTag, string keytag, BaseViewModel viewModel = null, ILogger logger = null)
        {
            IAction action = null;
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    action = CreateAction(keytag, _lSW_ActionFactory, _lsw_DestroyableActions, viewModel, logger);
                    break;
                case WindowTag.WINDOW_TAG_MAIN_SCREEN:
                    action = CreateAction(keytag, _mSW_ActionFactory, _msw_DestroyableActions, viewModel, logger);
                    break;

            }
            return action;
        }


        private IAction GetKeyActionAndLockFactory(string windowTag, string keytag, bool isLock = false, FactoryStatus status = FactoryStatus.Default, BaseViewModel viewModel = null, ILogger logger = null)
        {
            IAction action = null;
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    action = CreateAction(keytag, _lSW_ActionFactory, _lsw_DestroyableActions, viewModel, logger);
                    _lSW_ActionFactory.LockFactory(status);
                    break;
                case WindowTag.WINDOW_TAG_MAIN_SCREEN:
                    action = CreateAction(keytag, _mSW_ActionFactory, _msw_DestroyableActions, viewModel, logger);
                    _mSW_ActionFactory.LockFactory(status);
                    break;

            }
            return action;
        }

        private IAction CreateAction(string keyTag
            , BaseCommandExecuterFactory factory
            , Dictionary<string, IAction> cacheActionList
            , BaseViewModel viewModel = null
            , ILogger logger = null
            , bool isDestroyableCommandExecuter = false)
        {
            IAction action;
            try
            {
                action = cacheActionList[keyTag];
            }
            catch
            {
                action = null;
            }

            if (action == null)
            {
                if (!factory.Locker.IsLock)
                {
                    if (viewModel != null)
                    {
                        if (!isDestroyableCommandExecuter)
                        {
                            action = factory.CreateViewModelCommandExecuter(keyTag, viewModel, logger);
                        }
                        else
                        {
                            action = factory.CreateDestroyableViewModelCommandExecuter(keyTag, viewModel, logger);
                            cacheActionList.Add(keyTag, action);
                        }
                    }
                    else
                    {
                        action = factory.CreateCommandExecuter(keyTag, logger);
                    }
                }
                else
                {
                    if (viewModel != null)
                    {
                        if (!isDestroyableCommandExecuter)
                        {
                            action = factory.CreateAlternativeViewModelCommandExecuterWhenFactoryIsLock(keyTag, viewModel, logger);
                        }
                        else
                        {
                            action = factory.CreateAlternativeDestroyableViewModelCommandExecuterWhenFactoryIsLock(keyTag, viewModel, logger);
                            cacheActionList.Add(keyTag, action);
                        }
                    }
                    else
                    {
                        action = factory.CreateAlternativeCommandExecuterWhenFactoryIsLock(keyTag, logger);
                    }
                }
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
