using DevExpress.Xpf.Editors;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    class DbManager
    {
        private static DbManager _instance;

        private SQLResultHandler _provider;

        private DbManager()
        {
            _provider = new SQLResultHandler();
        }

        public async Task ExecuteQueryAsync(string cmdKey, SQLQueryCustodian observer, params string[] paramaters )
        {
            _provider.Subcribe(observer);
            _provider.ExecuteQueryAsync(cmdKey, paramaters);
            if (observer.Updated)
            {
                _provider.Unsubcribe(observer);
            }
        }

        public static DbManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbManager();
                }
                return _instance;
            }
        }
    }
}
