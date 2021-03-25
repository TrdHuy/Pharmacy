using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage
{
    public class CustomerModificationPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerModificationPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        private Visibility _customerNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private ImageSource _customerImageSource;

        private bool _isSaveButtonRunning = false;

        #region Public properties
        public tblCustomer CurrentModifiedCustomer { get; set; }
        public string CustomerImageFileName { get; set; }
        public string CustomerID
        {
            get
            {
                return CurrentModifiedCustomer.CustomerID.ToString();
            }
        }

        public ImageSource CustomerImageSource
        {
            get
            {
                return _customerImageSource;
            }
            set
            {
                _customerImageSource = value;
                InvalidateOwn();
            }
        }

        public string CustomerNameText
        {
            get
            {
                return CurrentModifiedCustomer.CustomerName;
            }
            set
            {
                CurrentModifiedCustomer.CustomerName = value;
                CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                   Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string PhoneText
        {
            get
            {
                return CurrentModifiedCustomer.Phone;
            }
            set
            {
                CurrentModifiedCustomer.Phone = value;
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string AdressText
        {
            get
            {
                return CurrentModifiedCustomer.Address;
            }
            set
            {
                CurrentModifiedCustomer.Address = value;
                InvalidateOwn();
            }
        }
        public string EmailText
        {
            get
            {
                return CurrentModifiedCustomer.Email;
            }
            set
            {
                CurrentModifiedCustomer.Email = value;
                InvalidateOwn();
            }
        }
        public string DiscriptionText
        {
            get
            {
                return CurrentModifiedCustomer.CustomerDescription;
            }
            set
            {
                CurrentModifiedCustomer.CustomerDescription = value;
                InvalidateOwn();
            }
        }

        public Visibility CustomerNameAwareTextBlockVisibility
        {
            get
            {
                return _customerNameAwareTextBlockVisibility;
            }
            set
            {
                _customerNameAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility PhoneAwareTextBlockVisibility
        {
            get
            {
                return _phoneNameAwareTextBlockVisibility;
            }
            set
            {
                _phoneNameAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }


        public bool IsSaveButtonCanPerform
        {
            get
            {
                return CustomerNameAwareTextBlockVisibility != Visibility.Visible &&
                    PhoneAwareTextBlockVisibility != Visibility.Visible;
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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public EventHandleCommand GridSizeChangedCommand { get; set; }

        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            CancleButtonCommand = new RunInputCommand(CancleButtonClickEvent);
            CameraButtonCommand = new RunInputCommand(CameraButtonClickEvent);
            GridSizeChangedCommand = new EventHandleCommand(OnGridSizeChangedEvent);

            CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedCustomer.CustomerName) ?
                Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedCustomer.Phone) ?
                Visibility.Visible : Visibility.Collapsed;

            CustomerImageSource = FileIOUtil.
                GetBitmapFromName(CurrentModifiedCustomer.CustomerID.ToString(), FileIOUtil.CUSTOMER_IMAGE_FOLDER_NAME).
                ToImageSource();
        }

        protected override void OnInitialized()
        {
        }

        private void CameraButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON
                , dataTransfer);
        }
        private void CancleButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON
                , dataTransfer);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
        }


        private void OnGridSizeChangedEvent(object sender, EventArgs e, object paramaters)
        {
            Grid ctrl = (Grid)sender;
            Border avaBorder = (Border)((object[])paramaters)[0];

            if (avaBorder.RenderSize.Width >= avaBorder.RenderSize.Height)
            {
                ctrl.Width = avaBorder.RenderSize.Height;
            }
            else
            {
                ctrl.Width = avaBorder.RenderSize.Width;
            }
        }

    }
}
