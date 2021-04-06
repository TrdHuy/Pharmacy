using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs
{
    public class MSW_RP_ComprehensiveReportOV : BaseViewModel
    {
        public decimal TongGiaTriNhap { get; set; }
        public decimal TongGiaTriXuat { get; set; }
        public decimal TongNoKH { get; set; }
        public decimal TongNoNCC { get; set; }
        public decimal ThuBanHang { get; set; }
        public decimal ThuKhac { get; set; }
        public decimal TongChi { get; set; }
        public decimal LaiGop { get; set; }

        private BaseViewModel _parentModel;

        public MSW_RP_ComprehensiveReportOV(BaseViewModel parentVM)
        {
            _parentModel = parentVM;
        }

    }
}
