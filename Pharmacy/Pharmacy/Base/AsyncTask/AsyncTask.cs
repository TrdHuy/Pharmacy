using Pharmacy.Base.AsyncAction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncTask
{
    public class AsyncTask : IAsyncTask
    {
        private long _delayTime;
        private AsyncTaskResult _result;
        private bool _isCompleted;
        private bool _isCompletedCallback;
        private bool _isCanceled;

        private Action<AsyncTaskResult> _callback;
        private Func<Task<AsyncTaskResult>> _execute;
        private Func<bool> _canExecute;

        public long DelayTime { get => _delayTime; }
        public AsyncTaskResult Result { get => _result; }

        public Func<bool> CanExecute => _canExecute;

        public Func<Task<AsyncTaskResult>> Execute => _execute;

        public Action<AsyncTaskResult> CallbackHandler => _callback;

        bool IAsyncTask.IsCompletedCallback { get => _isCompletedCallback; set => _isCompletedCallback = value; }

        bool IAsyncTask.IsCompleted { get => _isCompleted; set => _isCompleted = value; }

        bool IAsyncTask.IsCanceled { get => _isCanceled; set => _isCanceled = value; }

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


        private static Stopwatch AsynTaskExecuteWatcher;

        public static async void AsyncExecute(IAsyncTask asyncTask, CancellationToken token)
        {
            if (asyncTask == null)
                return;

            var asyncTaskResult = new AsyncTaskResult(null, MessageAsyncTaskResult.Non);

            try
            {
                AsynTaskExecuteWatcher = Stopwatch.StartNew();

                var canExecute = asyncTask.CanExecute == null ? true : (bool)asyncTask.CanExecute?.Invoke();

                if (canExecute)
                {
                    try
                    {
                        asyncTaskResult = await Task.Run<AsyncTaskResult>(asyncTask.Execute, token);

                        if (token.IsCancellationRequested)
                        {
                            token.ThrowIfCancellationRequested();
                        }

                        if (asyncTaskResult != null && asyncTaskResult.MesResult == MessageAsyncTaskResult.Aborted)
                        {
                            throw new OperationCanceledException(asyncTaskResult.Messsage);
                        }

                        asyncTask.IsCompleted = true;
                    }
                    catch
                    {
                        asyncTask.IsCanceled = true;
                    }

                    AsynTaskExecuteWatcher.Stop();
                    long restLoadingTime = asyncTask.DelayTime - AsynTaskExecuteWatcher.ElapsedMilliseconds;
                    if (restLoadingTime > 0)
                    {
                        await Task.Delay(Convert.ToInt32(restLoadingTime));
                    }

                    asyncTask.CallbackHandler?.Invoke(asyncTaskResult);
                    asyncTask.IsCompletedCallback = true;
                }

            }
            catch (OperationCanceledException)
            {
                asyncTask.IsCompletedCallback = false;
                asyncTask.IsCompleted = false;
                asyncTask.IsCanceled = true;
            }
        }

    }
}
