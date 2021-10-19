using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage.OVs
{
    internal class MSW_IMP_MedicineOV : BaseViewModel
    {
        private string _medicineTextSearch;
        private tblMedicineType _medTypeSelected;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private CancellationTokenSource Cts { get; set; }

        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };
        public bool IsNeedToFilterAfterPropertyChanged = true;

        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                if (value != null && EndDate != null)
                {
                    FilterDataByDate();
                }
            }
        }
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                if (value != null && StartDate != null)
                {
                    FilterDataByDate();
                }
            }
        }

        public string MedicineTextSearch
        {
            get
            {
                return _medicineTextSearch;
            }
            set
            {
                _medicineTextSearch = value;
                if (IsNeedToFilterAfterPropertyChanged)
                {
                    OnSearchTextChangedEvent();
                }
                IsNeedToFilterAfterPropertyChanged = true;
                InvalidateOwn();
            }
        }


        public tblMedicineType SelectedMedType
        {
            get
            {
                return _medTypeSelected;
            }
            set
            {
                _medTypeSelected = value;
                if (IsNeedToFilterAfterPropertyChanged)
                {
                    OnSearchMedTypeChangedEvent();
                }
                IsNeedToFilterAfterPropertyChanged = true;
                InvalidateOwn();
            }
        }

        public MSW_IMP_MedicineOV(BaseViewModel parentVM)
        {
            ParentsModel = parentVM;
        }

        public override void RefreshViewModel()
        {
        }

        #region Filter by type
        private async void OnSearchMedTypeChangedEvent()
        {
            var model = (InventoryManagementPageViewModel)ParentsModel;
            var inventoryDGCache = model.InventoryDataGridCache;
            if (model.IsDataGridLoading)
            {
                if (Cts != null)
                    Cts.Cancel();
            }

            model.IsDataGridLoading = true;


            await Task.Delay(100);

            if (Cts == null)
            {
                Cts = new CancellationTokenSource();
            }

            var ct = Cts.Token;
            try
            {

                inventoryDGCache.Items.Filter = new Predicate<object>(med => FilterMedicineList2(med, _medTypeSelected.MedicineTypeName));
                model.IsDataGridLoading = false;
            }
            catch (OperationCanceledException ex)
            {
                if (Cts != null)
                    Cts.Dispose();
                Cts = null;
            }

        }

        private bool FilterMedicineList2(object med, string filterText)
        {
            return SearchByType(med, filterText);
        }

        private bool SearchByType(object med, string filterText)
        {
            dynamic r = med;
            return RUNE.IS_SUPPORT_FILTER_MEDICINE_BY_TYPE
                ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.MedType, filterText, CompareOptions.IgnoreCase) >= 0
                || CultureInfo.CurrentCulture.CompareInfo.IndexOf(filterText, "Tất cả", CompareOptions.IgnoreCase) >= 0)
                : false;
        }
        #endregion


        #region Filter by name, id
        private async void OnSearchTextChangedEvent()
        {
            var model = (InventoryManagementPageViewModel)ParentsModel;
            if (model.IsDataGridLoading)
            {
                if (Cts != null)
                    Cts.Cancel();
            }

            model.IsDataGridLoading = true;


            await Task.Delay(100);

            if (Cts == null)
            {
                Cts = new CancellationTokenSource();
            }

            var ct = Cts.Token;
            try
            {

                model.InventoryDataGridCache.Items.Filter = new Predicate<object>(med => FilterMedicineList(med, MedicineTextSearch));
                model.IsDataGridLoading = false;
            }
            catch (OperationCanceledException ex)
            {
                if (Cts != null)
                    Cts.Dispose();
                Cts = null;
            }

        }

        private bool FilterMedicineList(object med, string filterText)
        {
            return SearchByName(med, filterText) ||
                SearchByID(med, filterText);
        }

        private bool SearchByName(object med, string filterText)
        {
            dynamic r = med;
            return RUNE.IS_SUPPORT_SEARCH_ORDER_BY_MEDICINE_NAME ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.MedName, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }
        private bool SearchByID(object med, string filterText)
        {
            dynamic r = med;
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_ID ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.MedId, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }
        #endregion

        private void FilterDataByDate()
        {
            var model = (InventoryManagementPageViewModel)ParentsModel;
            model.IsDataGridLoading = true;

            if (DateTime.Compare(EndDate.Value, StartDate.Value) < 0)
            {
                App.Current.ShowApplicationMessageBox("Ngày kết thúc phải lớn hơn ngày bắt đầu!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo");
                model.IsDataGridLoading = false;
            }
            else
            {
                SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        model.InventoryDataSource = (ObservableCollection<object>)(queryResult.Result);
                    }
                    else
                    {
                        App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu kho hàng, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                            HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                            HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                            OwnerWindow.MainScreen,
                            "Lỗi!");
                    }
                    model.IsDataGridLoading = false;
                });
                DbManager.Instance.ExecuteQueryAsync(true, SQLCommandKey.GET_INVENTORY_DATA_CMD_KEY,
                   PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
                   _sqlCmdObserver,
                   StartDate,
                   EndDate.Value.AddDays(1));
            }
        }

    }
}

