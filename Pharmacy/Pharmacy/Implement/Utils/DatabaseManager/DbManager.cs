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

        private PharmacyDBContext _appDBContext;

        private DbManager()
        {
            _appDBContext = new PharmacyDBContext();
            _provider = new SQLResultHandler(_appDBContext);
        }

        public async void ExecuteQueryAsync(string cmdKey, int delayTime, SQLQueryCustodian observer, params object[] paramaters)
        {
            _provider.Subcribe(observer);

            // Issue: when first boost app, query function make the app delay for a while
            await Task.Delay(delayTime);

            var res = _provider.ExecuteQuery(cmdKey, paramaters);

            _provider.NotifyChange(res);

            // Issue: provider must un-subciribe observer coz it will callback next time
            if (observer.Updated)
            {
                _provider.Unsubcribe(observer);
            }

        }

        public void ExecuteQuery(string cmdKey, SQLQueryCustodian observer, params object[] paramaters)
        {
            _provider.Subcribe(observer);

            var res = _provider.ExecuteQuery(cmdKey, paramaters);

            _provider.NotifyChange(res);

            if (observer.Updated)
            {
                _provider.Unsubcribe(observer);
            }
        }

        public async void ExecuteQueryAsync(bool isUsingSeprateProvider, string cmdKey, int delayTime, SQLQueryCustodian observer, params object[] paramaters)
        {
            if (isUsingSeprateProvider)
            {
                SQLResultHandler newProvider = new SQLResultHandler(_appDBContext);

                newProvider.Subcribe(observer);

                // Issue: when first boost app, query function make the app delay for a while
                await Task.Delay(delayTime);

                var res = newProvider.ExecuteQuery(cmdKey, paramaters);

                newProvider.NotifyChange(res);

                // Issue: provider must un-subciribe observer coz it will callback next time
                if (observer.Updated)
                {
                    newProvider.Unsubcribe(observer);
                }
            }
            else
            {
                ExecuteQueryAsync(cmdKey, delayTime, observer, paramaters);
            }

        }

        public void ExecuteQuery(bool isUsingSeprateProvider, string cmdKey, SQLQueryCustodian observer, params object[] paramaters)
        {
            if (isUsingSeprateProvider)
            {
                SQLResultHandler newProvider = new SQLResultHandler(_appDBContext);
                
                newProvider.Subcribe(observer);

                var res = newProvider.ExecuteQuery(cmdKey, paramaters);

                newProvider.NotifyChange(res);

                if (observer.Updated)
                {
                    newProvider.Unsubcribe(observer);
                }
            }
            else
            {
                ExecuteQuery(cmdKey, observer, paramaters);
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
