using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class KeyActionListener
    {
        private static KeyActionListener _instance;
        private KeyActionFactory _keyActionFactory;

        public void OnKey(string windowTag, string keyFeature, object[] dataTransfer)
        {
            IAction action = GetKeyActionType(windowTag, keyFeature);
            if (action != null)
            {
                action.Execute(dataTransfer);
            }
        }

        private IAction GetKeyActionType(string windowTag, object obj)
        {
            switch (windowTag)
            {
                case WindowTag.WINDOW_TAG_LOGIN_SCREEN:
                    _keyActionFactory = new LSW_ActionFactory();
                    break;
                default:
                    return null;
            }

            return _keyActionFactory.CreateAction(obj);
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
