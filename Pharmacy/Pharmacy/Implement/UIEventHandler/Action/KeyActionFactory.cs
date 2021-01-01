using Pharmacy.Base.UIEventHandler.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.UIEventHandler.Action
{
    public abstract class KeyActionFactory : IActionFactory
    {
        protected FactoryLocker _locker = new FactoryLocker(LockReason.Unlock,false);

        public virtual FactoryLocker Locker { get => _locker; set => _locker = value; }

        public IAction CreateAction(object obj)
        {
            IAction action = null;
            string keyTag = (string)obj;

            if (!Locker.IsLock)
            {
                action = CreateActionFromCurrentWindow(keyTag);
            }
            else
            {
                action = CreateAlternativeActionFromCurrentWindow(keyTag);
            }
            return action;
        }

        protected abstract IAction CreateAlternativeActionFromCurrentWindow(string keyTag);

        protected abstract IAction CreateActionFromCurrentWindow(string keyTag);

        public virtual void LockFactory(bool key, LockReason reason)
        {
            Locker.IsLock = key;
            Locker.Reason = reason;
        }

    }
}
