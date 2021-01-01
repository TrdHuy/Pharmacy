﻿using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Definitions
{
    public static class PharmacyDefinitions
    {
        public static readonly int MINIMUM_PASSWORD_LENGHT = 8;
        public static readonly char[] SPECIAL_CHARS_OF_PASSWORD = "!#$%&'()*+,-./:;<=>?@[]^_`{|}~".ToCharArray();

        public static readonly int LOGIN_BUTTON_PERFORM_DELAY_TIME = 500;
        public static readonly int SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME = 1000;


        public const string HOME_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/Home/HomePage.xaml";
        public const string SELLING_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/Selling/SellingPage.xaml";
        public const string PERSONAL_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/PersonalInfo/PersonalInfoPage.xaml";
        public const string USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/UserManagement/UserManagementPage.xaml";
        public const string CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/CustomerManagement/CustomerManagementPage.xaml";
        public const string SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/SupplierManagement/SupplierManagementPage.xaml";
        public const string INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/InvoiceManagement/InvoiceManagementPage.xaml";
        public const string MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/MedicineManagement/MedicineManagementPage.xaml";
        public const string OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/OtherPaymentManagement/OtherPaymentsManagementPage.xaml";
        public const string WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/WarehouseManagement/WarehouseManagementPage.xaml";
        public const string REPORT_PAGE_URI_ORIGINAL_STRING = "/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/Report/ReportPage.xaml";


        public static readonly long HOME_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long SELLING_PAGE_LOADING_DELAY_TIME = 100;
        public static readonly long PERSONAL_INFO_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long USER_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long CUSTOMER_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long SUPPLIER_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long INVOICE_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long MEDICINE_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long OTHER_PAYMENT_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long WAREHOUSE_MANAGEMENT_PAGE_LOADING_DELAY_TIME = 2000;
        public static readonly long REPORT_PAGE_LOADING_DELAY_TIME = 2000;



    }

    public enum NewPasswordAwareMessage
    {
        [StringValue("Mật khẩu không được chứa khoảng trắng!")]
        WhiteSpaceAware = 1,
        [StringValue("Mật khẩu mới không được trùng mật khẩu hiện tại!")]
        SameBefore = 2,
        [StringValue("Mật khẩu tối thiểu 8 ký tự!")]
        NotMeetLenght = 3,
        [StringValue("Mật khẩu phải có ít nhất 1 trong các ký tự !#$%&'()*+,-./:;<=>?@[]^_`{|}~")]
        WrongFormat = 4
    }
}