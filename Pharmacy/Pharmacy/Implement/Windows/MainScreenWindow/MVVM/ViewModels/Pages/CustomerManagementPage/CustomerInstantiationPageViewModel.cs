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
    public class CustomerInstantiationPageViewModel : AbstractViewModel
    {
        private static Logger logger = new Logger("CustomerInstantiationPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        private Visibility _customerNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private ImageSource _customerImageSource;
        private tblCustomer _newCustomer;

        private bool _isSaveButtonRunning = false;

        #region Public properties
        public tblCustomer NewCustomer
        {
            get
            {
                return _newCustomer;
            }
            set
            {
                _newCustomer = value;
                _newCustomer.IsActive = true;
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
                return NewCustomer.CustomerName;
            }
            set
            {
                NewCustomer.CustomerName = value;
                CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                   Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string PhoneText
        {
            get
            {
                return NewCustomer.Phone;
            }
            set
            {
                NewCustomer.Phone = value;
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string AdressText
        {
            get
            {
                return NewCustomer.Address;
            }
            set
            {
                NewCustomer.Address = value;
                InvalidateOwn();
            }
        }
        public string EmailText
        {
            get
            {
                return NewCustomer.Email;
            }
            set
            {
                NewCustomer.Email = value;
                InvalidateOwn();
            }
        }
        public string DiscriptionText
        {
            get
            {
                return NewCustomer.CustomerDescription;
            }
            set
            {
                NewCustomer.CustomerDescription = value;
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
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
        public string CustomerImageFileName { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public EventHandleCommand GridSizeChangedCommand { get; set; }
        #endregion

        protected override void InitPropertiesRegistry()
        {
        }

        public CustomerInstantiationPageViewModel()
        {
            logger.I("Instantinating CustomerInstantiationPageViewModel");

            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            CancleButtonCommand = new RunInputCommand(CancleButtonClickEvent);
            CameraButtonCommand = new RunInputCommand(CameraButtonClickEvent);
            NewCustomer = new tblCustomer();

            CustomerImageSource = Pharmacy.Properties.Resources.customer_default_icon.ToImageSource();

            CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.CustomerName) ?
                Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.Phone) ?
                Visibility.Visible : Visibility.Collapsed;

            GridSizeChangedCommand = new EventHandleCommand(OnGridSizeChangedEvent);

            logger.I("Instantinated CustomerInstantiationPageViewModel");
        }

        private void CameraButtonClickEvent(object paramaters)
        {
            logger.V("OnCameraButtonClick");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON
                , dataTransfer);
        }
        private void CancleButtonClickEvent(object paramaters)
        {
            logger.V("OnCancleButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON
                , dataTransfer);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            logger.V("OnSaveButtonClickEvent");

            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
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
