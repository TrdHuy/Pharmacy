using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncTask
{
    public class AsyncTaskExecuter
    {
        private static Stopwatch AsynTaskExecuteWatcher;

        public static async void AsyncExecute(IAsyncTask asyncTask)
        {
            AsynTaskExecuteWatcher = Stopwatch.StartNew();

            var canExecute = asyncTask.CanExecute == null ? true : (bool)asyncTask.CanExecute?.Invoke();

            if (canExecute)
            {
                var asyncTaskResult = asyncTask.Execute == null ? 
                    new AsyncTaskResult(null, MessageAsyncTaskResult.Non)
                    : await asyncTask.Execute?.Invoke();

                AsynTaskExecuteWatcher.Stop();
                long restLoadingTime = asyncTask.DelayTime - AsynTaskExecuteWatcher.ElapsedMilliseconds;
                if (restLoadingTime > 0)
                {
                    await Task.Delay(Convert.ToInt32(restLoadingTime));
                }

                asyncTask.CallbackHandler?.Invoke(asyncTaskResult);
            }
        }
    }
}
