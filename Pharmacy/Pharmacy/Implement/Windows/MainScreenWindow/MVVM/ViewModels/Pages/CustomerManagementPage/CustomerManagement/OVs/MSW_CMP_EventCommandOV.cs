using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerManagement.OVs
{
    public class MSW_CMP_EventCommandOV : BaseViewModel
    {
        private CancellationTokenSource Cts { get; set; }
        private DataGrid customerDGCache { get; set; }

        public EventCommandModel SearchTextChangedCommand { get; set; }
        

        public MSW_CMP_EventCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SearchTextChangedCommand = new EventCommandModel(OnSearchTextChangedEvent);
        }

        private async void OnSearchTextChangedEvent(object sender, EventArgs e, object paramaters)
        {
            var model = (CustomerManagementPageViewModel)ParentsModel;
            if (model.IsDataGridLoading)
            {
                if (Cts != null)
                    Cts.Cancel();
            }

            model.IsDataGridLoading = true;
            if(customerDGCache == null)
            {
                customerDGCache = (DataGrid)((object[])paramaters)[0];
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
                await Task.Delay(model.DelayTextChangedHandler, ct);

                customerDGCache.Items.Filter = new Predicate<object>(customer => FilterCustomerList(customer as tblCustomer, ctrl.Text));
                model.IsDataGridLoading = false;
            }
            catch (OperationCanceledException ex)
            {
                if (Cts != null)
                    Cts.Dispose();
                Cts = null;
            }

        }

        private bool FilterCustomerList(tblCustomer customer, string filterText)
        {
            return SearchByName(customer, filterText) ||
                SearchByPhone(customer, filterText) ||
                SearchByEmail(customer, filterText) ||
                SearchByAddress(customer, filterText);
        }

        private bool SearchByName(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME ? (customer.CustomerName.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByPhone(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE ? (customer.Phone.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByEmail(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_EMAIL ? (customer.Email.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByAddress(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_ADDRESS ? (customer.Address.IndexOf(filterText) != -1) : false;
        }

        public override void OnDestroy()
        {
            customerDGCache.Items.Filter = null;
            customerDGCache = null;
            base.OnDestroy();
        }
    }
}
