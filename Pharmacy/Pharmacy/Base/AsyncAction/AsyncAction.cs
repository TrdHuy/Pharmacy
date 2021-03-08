using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncAction
{
    public class AsyncAction : IAsyncAction
    {
        private object[] _paramaters;
        private long _delayTime;
        private AsyncActionResult _result;

        private Func<AsyncActionResult, object, bool> _callback;
        private Func<AsyncActionResult, object, bool> _execute;
        private Func<AsyncActionResult, object, bool> _canExecute;

        public long DelayTime { get => _delayTime; }
        public object[] Paramaters { get => _paramaters; }
        public AsyncActionResult Result { get => _result; }

        public AsyncAction(object[] paramater, Func<AsyncActionResult, object, bool> execute)
        {
            InitializeAsyncAction(paramater, execute);
        }

        public AsyncAction(object[] paramater, Func<AsyncActionResult, object, bool> execute, Func<AsyncActionResult, object, bool> canExecute)
        {
            InitializeAsyncAction(paramater, execute, canExecute);
        }

        public AsyncAction(object[] paramater, Func<AsyncActionResult, object, bool> execute, Func<AsyncActionResult, object, bool> canExecute, Func<AsyncActionResult, object, bool> callback)
        {
            InitializeAsyncAction(paramater, execute, canExecute, callback);
        }

        public AsyncAction(object[] paramater, Func<AsyncActionResult, object, bool> execute, Func<AsyncActionResult, object, bool> canExecute, Func<AsyncActionResult, object, bool> callback, long delayTime)
        {
            InitializeAsyncAction(paramater, execute, canExecute, callback, delayTime);
        }

        private void InitializeAsyncAction(object[] paramater,
            Func<AsyncActionResult, object, bool> execute,
            Func<AsyncActionResult, object, bool> canExecute = null,
            Func<AsyncActionResult, object, bool> callback = null,
            long delayTime = 0)
        {
            _paramaters = paramater;
            _execute = execute;
            _canExecute = canExecute;
            _callback = callback;
            _delayTime = delayTime;
            _result = new AsyncActionResult(null, MessageAsyncActionResult.Non);
        }

        public bool CallbackHandler()
        {
            return _callback == null ? false : _callback.Invoke(_result, Paramaters);
        }

        public bool CanExecute()
        {
            return _canExecute == null ? true : _canExecute.Invoke(_result, Paramaters);
        }

        public bool Execute()
        {
            return _execute == null ? false : _execute.Invoke(_result, Paramaters);
        }
    }
}
