﻿using Pharmacy.Base.UIEventHandler.Action;
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
                default:
                    action = null;
                    break;
            }
            return action;
        }
    }
}