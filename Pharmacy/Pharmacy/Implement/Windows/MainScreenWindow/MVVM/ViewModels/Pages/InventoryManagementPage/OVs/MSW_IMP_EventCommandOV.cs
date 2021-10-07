using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage.OVs
{
    internal class MSW_IMP_EventCommandOV : BaseViewModel
    {
        private CancellationTokenSource Cts { get; set; }
        private DataGrid inventoryDGCache { get; set; }

        public EventCommandModel SearchTextChangedCommand { get; set; }
        public EventCommandModel SearchMedTypeCommand { get; set; }

        
        public MSW_IMP_EventCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SearchTextChangedCommand = new EventCommandModel(OnSearchTextChangedEvent);
            SearchMedTypeCommand = new EventCommandModel(OnSearchMedTypeChangedEvent);

        }

        private async void OnSearchMedTypeChangedEvent(object sender, EventArgs e, object paramaters)
        {
            var model = (InventoryManagementPageViewModel)ParentsModel;
            if (model.IsDataGridLoading)
            {
                if (Cts != null)
                    Cts.Cancel();
            }

            model.IsDataGridLoading = true;
            if (inventoryDGCache == null)
            {
                inventoryDGCache = (DataGrid)((object[])paramaters)[0];
            }
            HorusBox ctrl = (HorusBox)sender;

            await Task.Delay(100);

            if (Cts == null)
            {
                Cts = new CancellationTokenSource();
            }

            var ct = Cts.Token;
            try
            {

                inventoryDGCache.Items.Filter = new Predicate<object>(med => FilterMedicineList2(med, ctrl.Text));
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

        #region Search by name, id
        private async void OnSearchTextChangedEvent(object sender, EventArgs e, object paramaters)
        {
            var model = (InventoryManagementPageViewModel)ParentsModel;
            if (model.IsDataGridLoading)
            {
                if (Cts != null)
                    Cts.Cancel();
            }

            model.IsDataGridLoading = true;
            if (inventoryDGCache == null)
            {
                inventoryDGCache = (DataGrid)((object[])paramaters)[0];
            }
            TextBox ctrl = (TextBox)sender;

            await Task.Delay(100);

            if (Cts == null)
            {
                Cts = new CancellationTokenSource();
            }

            var ct = Cts.Token;
            try
            {

                inventoryDGCache.Items.Filter = new Predicate<object>(med => FilterMedicineList(med, ctrl.Text));
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
                SearchByID(med, filterText) ;
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

        public override void OnDestroy()
        {
            if (inventoryDGCache != null)
            {
                inventoryDGCache.Items.Filter = null;
                inventoryDGCache = null;
            }
            base.OnDestroy();
        }
    }
}
