using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    class SQLQueryCustodian : Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>
    {
        private Action<object> _callback;

        public SQLQueryCustodian(Action<object> callback)
        {
            _callback = callback;
        }

        public bool Updated { get; set; } = false;

        public virtual void Update(SQLQueryResult queryResult)
        {
            _callback?.Invoke(queryResult);
        }

    }
}
