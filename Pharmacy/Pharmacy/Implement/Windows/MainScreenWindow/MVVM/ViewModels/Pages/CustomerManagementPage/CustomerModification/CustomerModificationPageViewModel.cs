using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerModification.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerModification
{
    internal class CustomerModificationPageViewModel : MSW_BasePageViewModel
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
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public MSW_CMP_CMoP_ButtonCommandOV ButtonCommandOV { get; set; }
        public EventCommandModel GridSizeChangedCommand { get; set; }

        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;

            ButtonCommandOV = new MSW_CMP_CMoP_ButtonCommandOV(this);
            GridSizeChangedCommand = new EventCommandModel(OnGridSizeChangedEvent);
        }

        protected override void OnInitialized()
        {
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

        public override void OnLoaded()
        {
            base.OnLoaded();
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;

            CustomerImageSource = FileIOUtil.
                GetBitmapFromName(CurrentModifiedCustomer.CustomerID.ToString(), FileIOUtil.CUSTOMER_IMAGE_FOLDER_NAME).
                ToImageSource();

            CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedCustomer.CustomerName) ?
               Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedCustomer.Phone) ?
                Visibility.Visible : Visibility.Collapsed;
        }

    }
}
