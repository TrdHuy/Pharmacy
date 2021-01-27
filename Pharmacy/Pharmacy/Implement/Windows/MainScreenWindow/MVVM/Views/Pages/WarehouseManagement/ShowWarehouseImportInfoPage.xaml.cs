﻿using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.WarehouseManagement
{
    /// <summary>
    /// Interaction logic for ShowWarehouseImportInfoPage.xaml
    /// </summary>
    public partial class ShowWarehouseImportInfoPage : Page
    {
        public ShowWarehouseImportInfoPage()
        {
            InitializeComponent();
            DataContext = new ShowWarehouseImportInfoPageViewModel();
        }
    }
}
