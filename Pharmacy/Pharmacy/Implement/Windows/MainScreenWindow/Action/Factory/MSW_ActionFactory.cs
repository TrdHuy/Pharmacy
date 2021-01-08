using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerInstantiationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerModificationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserModificationPage;
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
                case KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON:
                    action = new MSW_UMP_EditButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON:
                    action = new MSW_UMP_DeleteUserButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON:
                    action = new MSW_UMP_AddNewUserButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON:
                    action = new MSW_UMP_UMoP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON:
                    action = new MSW_UMP_UMoP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON:
                    action = new MSW_UMP_UIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON:
                    action = new MSW_UMP_UMoP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON:
                    action = new MSW_UMP_UIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON:
                    action = new MSW_UMP_UIP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON:
                    action = new MSW_MMP_DeleteMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON:
                    action = new MSW_MMP_AddNewMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON:
                    action = new MSW_MMP_ModifyMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON:
                    action = new MSW_CMP_AddButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON:
                    action = new MSW_CMP_CIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON:
                    action = new MSW_CMP_CIP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON:
                    action = new MSW_CMP_CIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON:
                    action = new MSW_CMP_EditButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON:
                    action = new MSW_CMP_DeleteButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON:
                    action = new MSW_CMP_HistoryButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON:
                    action = new MSW_CMP_CMoP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON:
                    action = new MSW_CMP_CMoP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON:
                    action = new MSW_CMP_CMoP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON:
                    action = new MSW_MMP_AMP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON:
                    action = new MSW_MMP_AMP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON:
                    action = new MSW_MMP_AMP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CAMERA_BUTTON:
                    action = new MSW_MMP_MMP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CANCEL_BUTTON:
                    action = new MSW_MMP_MMP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_SAVE_BUTTON:
                    action = new MSW_MMP_MMP_SaveButtonAction();
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
