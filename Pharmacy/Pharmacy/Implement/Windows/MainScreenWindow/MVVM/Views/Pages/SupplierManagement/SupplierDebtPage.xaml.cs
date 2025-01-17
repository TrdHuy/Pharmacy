﻿using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierDebt;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.SupplierManagement
{
    /// <summary>
    /// Interaction logic for SupplierDebtPage.xaml
    /// </summary>
    public partial class SupplierDebtPage : QuotableEventPage
    {
        public SupplierDebtPage()
        {
            DataContext = new SupplierDebtPageViewModel();
            InitializeComponent();
        }
    }
}
