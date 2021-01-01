using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory
{
    public class MSW_ActionFactory : KeyActionFactory
    {
        protected override IAction CreateActionFromCurrentWindow(string keyTag)
        {
            IAction action;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE:
                    action = new MSW_HomePageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO:
                    action = new MSW_PersonalInfoAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT:
                    action = new MSW_SellingAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT:
                    action = new MSW_UserManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT:
                    action = new MSW_CustomerManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT:
                    action = new MSW_SupplierManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT:
                    action = new MSW_InvoiceManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT:
                    action = new MSW_MedicineManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT:
                    action = new MSW_OtherPaymentsManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT:
                    action = new MSW_WarehouseManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_REPORT:
                    action = new MSW_Repor();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON:
                    action = new MSW_PIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON:
                    action = new MSW_PIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON:
                    action = new MSW_PIP_CameraButtonAction();
                    break;
                default:
                    action = null;
                    break;
            }
            return action;
        }

        protected override IAction CreateAlternativeActionFromCurrentWindow(string keyTag)
        {
            IAction action;
            switch (keyTag)
            {
                default:
                    action = new MSW_AlternativeAction();
                    break;
            }
            return action;
        }

    }


}
