using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs
{
    public class MSW_CMP_CTP_CBP_ButtonCommandOV : AbstractViewModel
    {
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
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
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
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
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand AddNewOrderDetailButtonCommand { get; set; }
        public RunInputCommand DeleteOrderDetailButtonCommand { get; set; }
        public RunInputCommand EditEnablerButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand RefreshOrderDetaisButtonCommand { get; set; }

        public MSW_CMP_CTP_CBP_ButtonCommandOV(AbstractViewModel parentVM) : base(parentVM)
        {
            EditEnablerButtonCommand = new RunInputCommand(EditEnablerButtonClickEvent);
            AddNewOrderDetailButtonCommand = new RunInputCommand(AddNewOrderDetailButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            DeleteOrderDetailButtonCommand = new RunInputCommand(DeleteOrderDetailButtonClickEvent);
            RefreshOrderDetaisButtonCommand = new RunInputCommand(RefreshOrderDetaisButtonClickEvent);
        }

        private void RefreshOrderDetaisButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_REFRESH_BUTTON
                , dataTransfer);
        }

        private void EditEnablerButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_EDIT_ENABLER_BUTTON
                , dataTransfer);
        }


        private void AddNewOrderDetailButtonClickEvent(object paramaters)
        {
            IsAddOrderDeatailButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_ADD_ORDER_DETAIL_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }


        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }


        private void DeleteOrderDetailButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_DELETE_ORDER_DETAIL_BUTTON
                , dataTransfer);
        }

    }
}
