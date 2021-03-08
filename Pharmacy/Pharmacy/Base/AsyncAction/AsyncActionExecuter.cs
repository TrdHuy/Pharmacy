using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncAction
{
    public class AsyncActionExecuter
    {
        private static Stopwatch AsynActionExecuteWatcher;

        public static void Execute(IAsyncAction asyncAction)
        {

            if (asyncAction.CanExecute())
            {
                try
                {
                    if (asyncAction.Execute())
                        asyncAction.CallbackHandler();
                }
                catch (Exception e)
                {
                    asyncAction.Result.MesResult = MessageAsyncActionResult.Aborted;
                    asyncAction.Result.Messsage = e.Message;
                }
            }

        }

        public static async void AsyncExecute(IAsyncAction asyncAction)
        {
            AsynActionExecuteWatcher = Stopwatch.StartNew();
            if (asyncAction.CanExecute())
            {
                try
                {

                    if (asyncAction.Execute())
                    {
                        AsynActionExecuteWatcher.Stop();
                        long restLoadingTime = asyncAction.DelayTime - AsynActionExecuteWatcher.ElapsedMilliseconds;

                        if (restLoadingTime > 0)
                        {
                            await Task.Delay(Convert.ToInt32(restLoadingTime));
                        }
                        asyncAction.CallbackHandler();
                    }
                }
                catch (Exception e)
                {
                    asyncAction.Result.MesResult = MessageAsyncActionResult.Aborted;
                    asyncAction.Result.Messsage = e.Message;
                }
            }
        }
    }
}
