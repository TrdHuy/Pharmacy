using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.AppImpl.Models.VOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_ProductInfoOV : BaseViewModel
    {
        [Bindable(true)]
        public string ProdIDText { get; set; }

        [Bindable(true)]
        public string ProdVersionText { get; set; }

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
