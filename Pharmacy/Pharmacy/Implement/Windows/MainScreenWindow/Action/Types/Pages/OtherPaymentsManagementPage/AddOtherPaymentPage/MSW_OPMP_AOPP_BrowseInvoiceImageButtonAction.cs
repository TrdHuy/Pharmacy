﻿using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage
{
    public class MSW_OPMP_AOPP_BrowseInvoiceImageButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private AddOtherPaymentPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as AddOtherPaymentPageViewModel;

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh hóa đơn!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        _viewModel.InvoiceImageURL = file;
                        _viewModel.Invalidate("InvoiceImageURL");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
                return false;
            }

            return true;
        }
    }
}