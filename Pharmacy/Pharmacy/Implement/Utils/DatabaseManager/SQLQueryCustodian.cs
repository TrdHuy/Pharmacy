using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    public class SQLQueryCustodian : Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>
    {
        private static Dictionary<string, Dictionary<string, SQLQueryCustodian>> CUSTODIAN_REGISTRATIONS =
            new Dictionary<string, Dictionary<string, SQLQueryCustodian>>();

        private ResultHandler _callback;
        private ResultHandler _forceCallback;

        private bool _canUpdate = true;
        private bool _updated = false;

        private string _parentClassName;
        private string _callbackName;

        public bool Updated
        {
            get
            {
                return _updated;
            }
            private set
            {
                _updated = value;
            }
        }

        public static void DeactiveAllRegistrationsOfType(Type type)
        {
            var className = type.Name;
            if (CUSTODIAN_REGISTRATIONS.ContainsKey(className))
            {
                foreach (SQLQueryCustodian sQC in CUSTODIAN_REGISTRATIONS[className].Values)
                {
                    sQC._canUpdate = false;
                }
            }
        }

        public static bool IsAllCallbackHandled(Type type)
        {
            var className = type.Name;
            if (CUSTODIAN_REGISTRATIONS.ContainsKey(className))
            {
                if (CUSTODIAN_REGISTRATIONS[className].Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        public static void ClearRegister()
        {
            CUSTODIAN_REGISTRATIONS.Clear();
        }

        // Set all un-executed callback to be unable to update
        public static void DeactiveAllExistedCurrentCustodian()
        {
            foreach (Dictionary<string, SQLQueryCustodian> sQCs in CUSTODIAN_REGISTRATIONS.Values)
            {
                foreach(SQLQueryCustodian sQC in sQCs.Values)
                {
                    sQC._canUpdate = false;
                }
            }
        }

        public SQLQueryCustodian(ResultHandler callback, ResultHandler forceCallback = null, Type callerType = null)
        {
            if (callerType == null)
            {
                callerType = typeof(SQLQueryCustodian);
            }

            if (callback != null)
            {
                _callback = callback;
                _parentClassName = callerType.Name;
                _callbackName = callback.Method.Name;
                Register(_parentClassName, _callbackName, this);
            }

            _forceCallback = forceCallback;
        }

        public virtual void Update(SQLQueryResult queryResult)
        {
            if (_canUpdate)
            {
                _callback?.Invoke(queryResult);
                Updated = true;
            }
            else
            {
                _forceCallback?.Invoke(queryResult);
            }
            Unregister(_parentClassName, _callbackName);
        }

        private void Register(string callerClassName, string callbackName, SQLQueryCustodian custodian)
        {

            // override and set updateable for callback method
            if (CUSTODIAN_REGISTRATIONS.ContainsKey(callerClassName))
            {
                if (CUSTODIAN_REGISTRATIONS[callerClassName].ContainsKey(callbackName))
                {
                    CUSTODIAN_REGISTRATIONS[callerClassName][callbackName]._canUpdate = false;
                    CUSTODIAN_REGISTRATIONS[callerClassName].Remove(callbackName);
                    CUSTODIAN_REGISTRATIONS[callerClassName].Add(callbackName, custodian);
                }
                else
                {
                    CUSTODIAN_REGISTRATIONS[callerClassName].Add(callbackName, custodian);
                }
            }
            else
            {
                var x = new Dictionary<string, SQLQueryCustodian>();
                x.Add(callbackName, custodian);
                CUSTODIAN_REGISTRATIONS.Add(callerClassName, x);
            }
        }


        private void Unregister(string callerClassName, string callbackName)
        {
            if (CUSTODIAN_REGISTRATIONS.ContainsKey(callerClassName))
            {
                if (CUSTODIAN_REGISTRATIONS[callerClassName].ContainsKey(callbackName))
                {
                    CUSTODIAN_REGISTRATIONS[callerClassName].Remove(callbackName);
                }
            }
        }
    }

    public delegate void ResultHandler(SQLQueryResult queryResult);
}
