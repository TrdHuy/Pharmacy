using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncAction
{
    public enum MessageAsyncActionResult
    {
        Non = 0x000000,

        // The task has done, but there is no result return
        OK = 1,

        // Done the task, and return the result
        Done = 2,

        // Finished the task, but return the null
        Finished = 3,

        // The task was aborted
        Aborted = 4,

        // The task was cancled
        Cancled = 5
    }

    public class AsyncActionResult
    {
        private object _result;
        private MessageAsyncActionResult _mesResult;
        private string _messageToString;

        public AsyncActionResult(object result, MessageAsyncActionResult mesResult, string messageToString = "")
        {
            _result = result;
            _mesResult = mesResult;
            _messageToString = messageToString;
        }

        public object Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public MessageAsyncActionResult MesResult
        {
            get { return _mesResult; }
            set { _mesResult = value; }

        }

        public string Messsage
        {
            get { return _messageToString; }
            set { _messageToString = value; }
        }
    }

}
