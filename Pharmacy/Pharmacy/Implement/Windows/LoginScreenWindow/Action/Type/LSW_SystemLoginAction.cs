using Pharmacy.Base.UIEventHandler.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    class LSW_SystemLoginAction : IAction
    {
        public bool Execute(object[] dataTransfer)
        {
            PharmacyEntities en = new PharmacyEntities();
            if (en.tblUsers.Where(o => o.Name == "Admin").FirstOrDefault() != null)
                MessageBox.Show("Logged in");
            else
            {
                tblUser user = new tblUser();
                user.Name = "Admin";
                en.tblUsers.Add(user);
                en.SaveChanges();
            }
            return true;
        }
    }
}
