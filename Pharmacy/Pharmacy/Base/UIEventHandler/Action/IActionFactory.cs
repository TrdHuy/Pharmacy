using Pharmacy.Implement.Utils.Attributes;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface IActionFactory
    {
        IAction CreateAlternativeActionWhenFactoryIsLock(string keyTag);

        IAction CreateMainAction(string keyTag);

        void LockFactory(FactoryStatus status = FactoryStatus.Default);
        void UnlockFactory(FactoryStatus status = FactoryStatus.Default);

        FactoryLocker Locker { get; set; }
    }

    public class FactoryLocker
    {
        public FactoryStatus Status;
        public bool IsLock;

        public FactoryLocker(FactoryStatus status, bool key)
        {
            Status = status;
            IsLock = key;
        }
    }

    public enum FactoryStatus
    {
        [StringValue("...")]
        Default = 0,

        [StringValue("Tác vụ đang được xử lý, vui lòng đợi trong vài giây!")]
        TaskHandling = 1,

        [StringValue("Tác vụ hiện không khả dụng, vui lòng chọn tác vụ khác!")]
        NotAvailable = 2,

        [StringValue("Mở khóa tác vụ!")]
        Unlock = 3,

        [StringValue("Tác vụ đang được xử lý, vui lòng đợi trong giây lát!")]
        TaskHandlingButCanDispose = 4,
    }
}
