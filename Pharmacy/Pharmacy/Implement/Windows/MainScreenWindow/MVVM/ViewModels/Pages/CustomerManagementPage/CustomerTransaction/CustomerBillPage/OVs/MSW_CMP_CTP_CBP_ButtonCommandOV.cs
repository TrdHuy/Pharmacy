using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs
{
    internal class MSW_CMP_CTP_CBP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_CTP_CBP_ButtonCommandOV");
        private bool _isAddOrderDeatailButtonRunning;
        private bool _isSaveButtonRunning;

        public bool IsAddOrderDeatailButtonRunning
        {
            get
            {

                return _isAddOrderDeatailButtonRunning;
            }
            set
            {
                _isAddOrderDeatailButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsSaveButtonRunning
        {
            get
            {

                return _isSaveButtonRunning;
            }
            set
            {
                _isSaveButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public CommandExecuterModel AddNewOrderDetailButtonCommand { get; set; }
        public CommandExecuterModel DeleteOrderDetailButtonCommand { get; set; }
        public CommandExecuterModel EditEnablerButtonCommand { get; set; }
        public CommandExecuterModel SaveButtonCommand { get; set; }
        public CommandExecuterModel RefreshOrderDetaisButtonCommand { get; set; }
        public CommandExecuterModel CancelButtonCommand { get; set; }
        public CommandExecuterModel PrintInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_CMP_CTP_CBP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            EditEnablerButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_EDIT_ENABLER_BUTTON
                    , paramaters);
            });

            AddNewOrderDetailButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsAddOrderDeatailButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_ADD_ORDER_DETAIL_BUTTON
                    , paramaters
                    , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });

            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_SAVE_BUTTON
                    , paramaters
                    , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });

            DeleteOrderDetailButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_DELETE_ORDER_DETAIL_BUTTON
                    , paramaters);
            });

            RefreshOrderDetaisButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_REFRESH_BUTTON
                    , paramaters);
            });

            CancelButtonCommand = new CommandExecuterModel(paramaters =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_CANCEL_BUTTON
                , paramaters);
            });

            PrintInvoiceButtonCommand = new CommandExecuterModel(paramaters =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_PRINT_INVOICE_BUTTON
                  , paramaters);
            });
        }

    }
}
