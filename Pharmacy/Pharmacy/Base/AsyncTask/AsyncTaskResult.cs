using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncTask
{
    public enum MessageAsyncTaskResult
    {
        Non = 0,

        /// <summary>
        /// The task has done, but there is no result return
        /// </summary>
        OK = 1,

        /// <summary>
        /// Done the task, and return the result
        /// </summary>
        Done = 2,

        /// <summary>
        /// Finished the task, but return the null
        /// </summary>
        Finished = 3,

        /// <summary>
        /// The task was aborted
        /// </summary>
        Aborted = 4,
    }

    public class AsyncTaskResult
    {
        private object _result;
        private MessageAsyncTaskResult _mesResult;
        private string _messageToString;

        public AsyncTaskResult(object result, MessageAsyncTaskResult mesResult, string messageToString = "")
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

        public MessageAsyncTaskResult MesResult
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
