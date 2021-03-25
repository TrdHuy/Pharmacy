using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerInstantiation.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerInstantiation
{
    internal class CustomerInstantiationPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerInstantiationPageViewModel");

        private Visibility _customerNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private ImageSource _customerImageSource;
        private tblCustomer _newCustomer;

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
        public MSW_CMP_CIP_ButtonCommandOV ButtonCommandOV { get; set; }
        public string CustomerImageFileName { get; set; }
        
        public EventCommandModel GridSizeChangedCommand { get; set; }

        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_CMP_CIP_ButtonCommandOV(this);

            NewCustomer = new tblCustomer();

            CustomerImageSource = Properties.Resources.customer_default_icon.ToImageSource();

            CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.CustomerName) ?
                Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.Phone) ?
                Visibility.Visible : Visibility.Collapsed;

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


    }
}
