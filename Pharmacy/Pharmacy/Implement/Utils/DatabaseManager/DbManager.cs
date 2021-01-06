using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    public class DbManager
    {
        private static DbManager _instance;

        private SQLResultHandler _provider;

        private DbManager()
        {
            _provider = new SQLResultHandler();
        }

        public async void ExecuteQueryAsync(string cmdKey, int delayTime, SQLQueryCustodian observer, params object[] paramaters)
        {
            _provider.Subcribe(observer);

            // Issue: when first boost app, query function make the app delay for a while
            await Task.Delay(delayTime);

            _provider.ExecuteQuery(cmdKey, paramaters);

            // Issue: provider must un-subciribe observer coz it will callback next time
            if (observer.Updated)
            {
                _provider.Unsubcribe(observer);
            }

        }

        public void ExecuteQuery(string cmdKey, SQLQueryCustodian observer, params object[] paramaters)
        {
            _provider.Subcribe(observer);

            _provider.ExecuteQuery(cmdKey, paramaters);

            if (observer.Updated)
            {
                _provider.Unsubcribe(observer);
            }
        }

        public void RollBack()
        {
            _provider?.RollBack();
        }

        public void Dispose()
        {
            _provider?.Dispose();
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
