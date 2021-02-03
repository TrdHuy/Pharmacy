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
        private tblOrder _currentSelectedOrder;

        public ObservableCollection<tblOrder> OrderItemSource { get; set; }

        public tblCustomer CurrentModidifiedCustomer { get; set; }
        public tblOrder CurrentSelectedOrder
        {
            get
            {
                return _currentSelectedOrder;
            }
            set
            {
                _currentSelectedOrder = value;
                InvalidateOwn();
                Invalidate("CurrentSelectedOrderDetails");
            }
        }

        public string CurrentSelectedOrderDetails
        {
            get
            {
                string orderDetails = "";
                if (CurrentSelectedOrder != null)
                {
                    var listDetails = CurrentSelectedOrder.tblOrderDetails;
                    var tabPos = GetTabPosAsString(ComputeTabPos());
                    var stringFormat = "{0," + tabPos[0] + "}" +
                        "{1," + tabPos[1] + "}" +
                        "{2," + tabPos[2] + "} " +
                        "{3}";

                    orderDetails += String.Join("\n", listDetails.Select((content, a) =>
                        String.Format(stringFormat
                        , content.tblMedicine.MedicineName
                        , content.Quantity
                        , content.tblMedicine.tblMedicineUnit.MedicineUnitName
                        , content.TotalPrice)
                   ));
                }

                return orderDetails;
            }
        }


        // Temp save this method to handle tab stop position
        private int[] ComputeTabPos()
        {
            var offsetSpaceP1 = 5;
            var offsetSpace = 3;

            var listDetails = CurrentSelectedOrder.tblOrderDetails;
            var pos1 = listDetails?.Aggregate((o1, o2) => o1.tblMedicine.MedicineName.Length
            > o2.tblMedicine.MedicineName.Length ? o1 : o2).
            tblMedicine.MedicineName.Length;

            pos1 += offsetSpaceP1;

            var pos2 = listDetails?.Aggregate((o1, o2) => o1.Quantity.ToString().Length
            > o2.Quantity.ToString().Length ? o1 : o2).
            Quantity.ToString().Length;

            pos2 += offsetSpace;

            var pos3 = listDetails?.Aggregate((o1, o2) => o1.tblMedicine.tblMedicineUnit.MedicineUnitName.Length
            > o2.tblMedicine.tblMedicineUnit.MedicineUnitName.Length ? o1 : o2).
            tblMedicine.tblMedicineUnit.MedicineUnitName.Length;

            pos3 += offsetSpace;

            var resPos = new int[] { pos1 ?? 0, pos2 ?? 0, pos3 ?? 0 };

            return resPos;
        }

        private string[] GetTabPosAsString(int[] tabPos)
        {
            var lenght = tabPos.Length;
            var resPos = new string[lenght];
            for (int i = 0; i < lenght; i++)
            {
                // alight to the right, hence must concat char '-'
                resPos[i] = "-" + tabPos[i] + "";
            }
            return resPos;
        }

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
            foreach (tblOrder order in CurrentModidifiedCustomer.tblOrders)
            {
                OrderItemSource.Add(order);
            }

        }

    }
}
