using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage.OVs
{
    internal class MSW_IMP_MedicineOV : BaseViewModel
    {
        private BaseViewModel _parentModel;
        private tblMedicine _curSelectedMedicine;
        private string _medicineTextSearch;

        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };

        public string MedicineTextSearch
        {
            get
            {
                return _medicineTextSearch;
            }
            set
            {
                _medicineTextSearch = value;
                InvalidateOwn();
            }
        }


        public tblMedicine CurrentSelectedMedicine
        {
            get
            {
                return _curSelectedMedicine;
            }
            set
            {
                _curSelectedMedicine = value;
                InvalidateOwn();
            }
        }
        public MSW_IMP_MedicineOV(BaseViewModel parentVM)
        {
            _parentModel = parentVM;
        }

        public override void RefreshViewModel()
        {
            MedicineTextSearch = "";
            CurrentSelectedMedicine = null;

        }
    }
}
