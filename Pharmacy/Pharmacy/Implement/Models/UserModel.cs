using Pharmacy.Base.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Models
{
    public class UserModel : AbstractBaseModel
    {
        private tblUser _privateData;

        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string Link { get; set; }
        public string NickName { get; set; }
        public string Job { get; set; }

        public List<tblOrder> tblOrders { get; set; }

        public UserModel()
        {

        }

        public UserModel(tblUser userData)
        {
            this._privateData = userData;
            this.Username = userData.Username;
            this.Password = userData.Password;
            this.FullName = userData.FullName;
            this.Email = userData.Email;
            this.Phone = userData.Phone;
            this.Address = userData.Address;
            this.IsAdmin = userData.IsAdmin;
            this.IsActive = userData.IsActive;
            this.Link = userData.Link;
            this.NickName = userData.NickName;
            this.Job = userData.Job;
            tblOrders = new List<tblOrder>(userData.tblOrders);
        }

        public override object Clone()
        {
            return _privateData != null ? new UserModel(_privateData) : new UserModel();
        }
    }
}
