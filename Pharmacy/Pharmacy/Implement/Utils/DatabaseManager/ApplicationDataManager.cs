using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    public class ApplicationDataManager
    {
        private static ApplicationDataManager _instance;

        public string ConnectionID { get; set; }
        public string SessionID { get; set; }
        public tblUser CurrentUser { get; set; }
        
        public void UpdateSessionInfo(string con, string ses, tblUser curUser)
        {
            ConnectionID = con;
            SessionID = ses;
            CurrentUser = curUser;
        }

        public string GenerateConnectionID()
        {
            Random rd = new Random();
            int ssIDLenght = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUWXYZ0123456789abcdefghijklmnopqrstuwxyz";
            return new string(Enumerable.Repeat(chars,ssIDLenght)
                .Select(s => s[rd.Next(s.Length)])
                .ToArray());
        }
        public static ApplicationDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationDataManager();
                }
                return _instance;
            }
        }
    }
}
