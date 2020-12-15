using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class PersonalInfoPageViewModel : AbstractViewModel
    {
        public tblUser CurrentUser { get { return App.Current.CurrentUser; } }


        public PersonalInfoPageViewModel()
        {
        }

        protected override void InitPropertiesRegistry()
        {
            PropRegister("CurrentUser");
        }
    }
}
