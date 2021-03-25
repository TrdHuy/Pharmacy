using Pharmacy.Base.AsyncAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncTask
{
    public class AsyncTask : IAsyncTask
    {
        private long _delayTime;
        private AsyncTaskResult _result;

        private Action<AsyncTaskResult> _callback;
        private Func<Task<AsyncTaskResult>> _execute;
        private Func<bool> _canExecute;

        public long DelayTime { get => _delayTime; }
        public AsyncTaskResult Result { get => _result; }

        public Func<bool> CanExecute => _canExecute;

        public Func<Task<AsyncTaskResult>> Execute => _execute;

        public Action<AsyncTaskResult> CallbackHandler => _callback;

        public AsyncTask(Func<Task<AsyncTaskResult>> execute)
        {
            InitializeAsyncTask(execute);
        }

        public AsyncTask(Func<Task<AsyncTaskResult>> execute, Func<bool> canExecute)
        {
            InitializeAsyncTask(execute, canExecute);
        }

        public AsyncTask(Func<Task<AsyncTaskResult>> execute, Func<bool> canExecute, Action<AsyncTaskResult> callback)
        {
            InitializeAsyncTask(execute, canExecute, callback);
        }

        public AsyncTask(Func<Task<AsyncTaskResult>> execute, Func<bool> canExecute, Action<AsyncTaskResult> callback, long delayTime)
        {
            InitializeAsyncTask(execute, canExecute, callback, delayTime);
        }

        private void InitializeAsyncTask(
            Func<Task<AsyncTaskResult>> execute,
            Func<bool> canExecute = null,
            Action<AsyncTaskResult> callback = null,
            long delayTime = 0)
        {
            _execute = execute;
            _canExecute = canExecute;
            _callback = callback;
            _delayTime = delayTime;
            _result = new AsyncTaskResult(null, MessageAsyncTaskResult.Non);
        }


    }
}
