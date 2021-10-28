using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs
{
    public class MSW_CMP_CTP_CBP_OderDetailOV : OrderDetailOV
    {
        public MSW_CMP_CTP_CBP_OderDetailOV(BaseViewModel parent) : base(parent) { }

        protected override bool ShouldUpdateQuantity(string quantityToString)
        {
            if (!((CustomerBillPageViewModel)ParentsModel).IsLoaded) return true;

            try
            {
                var quantityLeft = 0d;
                var inputQuantity = Convert.ToDouble(quantityToString);
                var useQuantityMax = false;

                if (inputQuantity < 0) return false;

                SQLQueryCustodian queryObserver = new SQLQueryCustodian((res) =>
                {
                    quantityLeft = Convert.ToDouble(res.Result);
                    quantityLeft -= Quantity;
                    if (Quantity == 0)
                    {
                        var tmp = (ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder.tblOrderDetails.Where(o => o.MedicineID == MedicineID).FirstOrDefault();
                        if (tmp != null)
                        {
                            quantityLeft += tmp.Quantity;
                        }
                    }
                });
                DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_MEDICINE_QUANTITY_TILL_NOW_EXCEPT_EDITTING_ORDER_DETAIL_CMD_KEY,
                    queryObserver,
                    Medicine,
                    OrderDetailID);

                var optsSource = new List<OsirisButton>();

                if (inputQuantity > Quantity)
                {
                    if (inputQuantity - Quantity > quantityLeft && quantityLeft >= 0)
                    {
                        optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm tối đa" });
                        optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                        optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                        var result = App.Current.ShowApplicationMultiOptionMessageBox((quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn ")
                            + quantityLeft + "("
                            + MedicineUnitName + ")\n"

                            + "Bạn có muốn tiếp tục nhập?",
                        optsSource,
                        AnubisMessageImage.Question,
                        OwnerWindow.MainScreen,
                        "Thông báo!");
                        if (result == 2 || result == -1)
                        {
                            return false;
                        }
                        else if (result == 0)
                        {
                            useQuantityMax = true;
                        }
                    }
                    else if (quantityLeft < 0)
                    {
                        optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                        optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                        var result = App.Current.ShowApplicationMultiOptionMessageBox((quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn ")
                            + quantityLeft + "("
                            + MedicineUnitName + ")\n"

                            + "Bạn có muốn tiếp tục nhập?",
                        optsSource,
                        AnubisMessageImage.Question,
                        OwnerWindow.MainScreen,
                        "Thông báo!");
                        if (result == 1 || result == -1)
                        {
                            return false;
                        }
                    }
                }

                _quantityToString = useQuantityMax ? (Quantity + quantityLeft).ToString() : quantityToString;
                try
                {
                    Quantity = Convert.ToDouble(_quantityToString);
                }
                catch
                {
                    Quantity = 0d;
                }
                Invalidate("QuantityToString");

                return false;
            }
            catch (Exception e)
            {
                return base.ShouldUpdateQuantity(quantityToString);
            }
        }
    }
}
