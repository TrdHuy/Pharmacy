using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage
{
    public class CustomerTransactionHistoryPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblOrder> OrderItemSource { get; set; }
        public tblCustomer CurrentModidifiedCustomer { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public CustomerTransactionHistoryPageViewModel()
        {
            CurrentModidifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;

            InstantiateItems();
        }
   
        private void InstantiateItems()
        {
            OrderItemSource = new ObservableCollection<tblOrder>();
            foreach(tblOrder order in CurrentModidifiedCustomer.tblOrders)
            {
                OrderItemSource.Add(order);
            }

        }

    }
}
