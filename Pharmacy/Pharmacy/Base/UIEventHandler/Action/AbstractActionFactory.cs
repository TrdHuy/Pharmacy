using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractActionFactory : IActionFactory
    {
        protected FactoryLocker _locker = new FactoryLocker(FactoryStatus.Unlock, false);

        public virtual FactoryLocker Locker { get => _locker; set => _locker = value; }

        public abstract IAction CreateAlternativeActionWhenFactoryIsLock(string keyTag);

        public abstract IAction CreateMainAction(string keyTag);

        public void LockFactory(FactoryStatus status)
        {
            Locker.IsLock = true;
            Locker.Status = status;
        }

        public void UnlockFactory(FactoryStatus status)
        {
            Locker.IsLock = false;
            Locker.Status = status;
        }
    }
}
