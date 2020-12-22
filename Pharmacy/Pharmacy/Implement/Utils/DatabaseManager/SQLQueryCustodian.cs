using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    public class SQLQueryCustodian : Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>
    {
        private ResultHandler _callback;

        public SQLQueryCustodian(ResultHandler callback)
        {
            _callback = callback;
        }

        public bool Updated { get; set; } = false;

        public virtual void Update(SQLQueryResult queryResult)
        {
            _callback?.Invoke(queryResult);
            Updated = true;
        }

    }

    public delegate void ResultHandler(SQLQueryResult queryResult);
}
