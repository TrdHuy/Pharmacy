using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.AppImpl.Models.VOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_ProductInfoOV : BaseViewModel
    {
        private string _prodIDText;
        private string _prodVersionText;

        [Bindable(true)]
        public string ProdIDText
        {
            get
            {
                return _prodIDText;
            }
            set
            {
                _prodIDText = value;
                InvalidateOwn();
                Invalidate("ProdIDTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public string ProdVersionText
        {
            get
            {
                return _prodVersionText;
            }
            set
            {
                _prodVersionText = value;
                InvalidateOwn();
                Invalidate("ProdVersionTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public Visibility ProdVersionTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(ProdVersionText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        [Bindable(true)]
        public Visibility ProdIDTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(ProdIDText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool IsProductInfoMeetCondition
        {
            get
            {
                return !(string.IsNullOrEmpty(ProdIDText)
                    || string.IsNullOrEmpty(ProdVersionText));
            }
        }
        public override void RefreshViewModel()
        {
            ProdIDText = AppInfoVO.ProductID;
            ProdVersionText = AppInfoVO.ProductVersion;

            base.RefreshViewModel();
        }
    }
}
