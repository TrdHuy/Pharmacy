﻿using Pharmacy.Base.MVVM.ViewModels;
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

        public EventHandleCommand SearchTextChangedCommand { get; set; }
        private CancellationTokenSource Cts { get; set; }

        public MSW_CMP_EventCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SearchTextChangedCommand = new EventHandleCommand(OnSearchTextChangedEvent);
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

            DataGrid dg = (DataGrid)((object[])paramaters)[0];
            TextBox ctrl = (TextBox)sender;

            Cts = new CancellationTokenSource();
            var ct = Cts.Token;
            try
            {
                await Task.Delay(model.DelayTextChangedHandler, ct);
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                dg.Items.Filter = new Predicate<object>(customer => FilterCustomerList(customer as tblCustomer, ctrl.Text));
                model.IsDataGridLoading = false;
            }
            catch (OperationCanceledException ex)
            {
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
    }
}