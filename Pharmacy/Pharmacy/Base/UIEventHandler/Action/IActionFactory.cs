using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface IActionFactory
    {
        IAction CreateAction(object obj);

        void LockFactory(bool key, LockReason reason = LockReason.Default);

        FactoryLocker Locker { get; set; }
    }

    public class FactoryLocker
    {
        public LockReason Reason;
        public bool IsLock;

        public FactoryLocker(LockReason reason, bool key)
        {
            Reason = reason;
            IsLock = key;
        }
    }

    public enum LockReason
    {
        [StringValue("...")]
        Default = 0,
        
        [StringValue("Tác vụ đang được xử lý, vui lòng đợi trong vài giây!")]
        TaskHandling = 1,

        [StringValue("Tác vụ hiện không khả dụng, vui lòng chọn tác vụ khác!")]
        NotAvailable = 2,

        [StringValue("Mở khóa tác vụ!")]
        Unlock = 3
    }
}
