using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerInstantiation.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.ComponentModel;
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

        [Bindable(true)]
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
        
        [Bindable(true)]
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
       
        [Bindable(true)]
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
       
        [Bindable(true)]
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
       
        [Bindable(true)]
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
        
        [Bindable(true)]
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
      
        [Bindable(true)]
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
        
        [Bindable(true)]
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
       
        [Bindable(true)]
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
        
        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CustomerImageSource = Properties.Resources.customer_default_icon.ToImageSource();
            NewCustomer = new tblCustomer();
            ButtonCommandOV = new MSW_CMP_CIP_ButtonCommandOV(this);
        }
        
        protected override void OnInitialized()
        {
        }

        public override void OnPreviewBindingDataContextInCache()
        {
            base.OnPreviewBindingDataContextInCache();
            NewCustomer = new tblCustomer();
            CustomerImageSource = Properties.Resources.customer_default_icon.ToImageSource();
            CustomerImageFileName = "";
            CustomerNameAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.CustomerName) ?
               Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(NewCustomer.Phone) ?
                Visibility.Visible : Visibility.Collapsed;
        }
    }
}
