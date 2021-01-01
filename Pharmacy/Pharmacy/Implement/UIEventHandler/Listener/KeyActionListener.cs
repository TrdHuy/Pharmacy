using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class KeyActionListener : IActionListener
    {
        private static KeyActionListener _instance;
        private MSW_ActionFactory _mSW_ActionFactory;
        private LSW_ActionFactory _lSW_ActionFactory;

        private KeyActionListener()
        {
            _mSW_ActionFactory = new MSW_ActionFactory();
            _lSW_ActionFactory = new LSW_ActionFactory();
        }

        public void OnKey(string windowTag, string keyFeature, object[] dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void OnKey(string windowTag, string keyFeature, object[] dataTransfer, FactoryLocker locker)
        {
            IAction action = GetKeyActionAndLockFactory(windowTag, keyFeature, locker.IsLock, locker.Reason);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        public void LockMSW_ActionFactory(bool key,LockReason reason)
        {
            _mSW_ActionFactory.LockFactory(key, reason);
        }

        public void LockLSW_ActionFactory(bool key , LockReason reason)
        {
            _lSW_ActionFactory.LockFactory(key, reason);
        }

        public LockReason GetMSWFactoryLockReason()
        {
            return _mSW_ActionFactory.Locker.Reason;
        }
        public LockReason GetLSWFactoryLockReason()
        {
            return _lSW_ActionFactory.Locker.Reason;
        }

        private IAction GetKeyActionType(string windowTag, object obj)
        {
            IAction action = null;
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    action = _lSW_ActionFactory.CreateAction(obj);
                    break;
                case WindowTag.WINDOW_TAG_MAIN_SCREEN:
                    action = _mSW_ActionFactory.CreateAction(obj);
                    break;

            }
            return action;
        }

        private IAction GetKeyActionAndLockFactory(string windowTag, object obj, bool isLock = false, LockReason reason = LockReason.Default)
        {
            IAction action = null;
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    action = _lSW_ActionFactory.CreateAction(obj);
                    _lSW_ActionFactory.LockFactory(isLock, reason);
                    break;
                case WindowTag.WINDOW_TAG_MAIN_SCREEN:
                    action = _mSW_ActionFactory.CreateAction(obj);
                    _mSW_ActionFactory.LockFactory(isLock, reason);
                    break;
              
            }
            return action;
        }

        public static KeyActionListener Instance
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
