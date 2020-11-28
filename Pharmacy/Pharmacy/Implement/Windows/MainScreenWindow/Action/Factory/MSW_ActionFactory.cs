using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory
{
    public class MSW_ActionFactory : KeyActionFactory
    {
        public override IAction CreateActionFromCurrentWindow(string keyTag)
        {
            IAction action;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO:
                    action = new MSW_PersonalInfoAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_BUSINESS_MANAGEMENT:
                    action = new MSW_BusinessManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_STAFF_MANAGEMENT:
                    action = new MSW_StaffManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT:
                    action = new MSW_CustomerManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_VENDOR_MANAGEMENT:
                    action = new MSW_VendorManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SALE_MANAGEMENT:
                    action = new MSW_SaleManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_REPORT:
                    action = new MSW_Repor();
                    break;
                default:
                    action = null;
                    break;
            }
            return action;
        }
    }
}
