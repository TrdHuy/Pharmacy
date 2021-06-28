using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils
{
    public class Logger : ILogger
    {
        private enum LogLv
        {
            [StringValue("V")]
            VERBOSE = 0,

            [StringValue("I")]
            INFO = 1,

            [StringValue("D")]
            DEBUG = 2,

            [StringValue("W")]
            WARNING = 3,

            [StringValue("F")]
            FATAL = 4,

            [StringValue("E")]
            ERROR = 5
        }

        private const string TAG = "HPSS_PMC";
        private const int OLD_LOG_FILES_CAPACITY = 50;
        private static readonly SemaphoreSlim Mutex = new SemaphoreSlim(1);

        private static ObservableQueue<Task<bool>> TaskQueue { get; set; }
        private static StringBuilder _logBuilder { get; set; }
        private static StringBuilder _userLogBuilder { get; set; }
        private static string filePath { get; set; }
        private static string fileName { get; set; }
        private static string directory { get; set; }
        private static string folderName { get; set; }

        private string className { get; set; }
        private int PId { get; set; }
        private int TId { get; set; }

        static Logger()
        {

            TaskQueue = new ObservableQueue<Task<bool>>();
            var cast = TaskQueue as IEnumerable<Task<bool>>;
            ((INotifyCollectionChanged)cast).CollectionChanged += TaskQueueChanged;
#if DEBUG
            InitLogDebug();
#else
            InitUserLog();
#endif

            try
            {
                var dateTimeNow = DateTime.Now.ToString("ddMMyyHHmmss");
                var attribs = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
                if (attribs.Length > 0)
                {
                    folderName = ((AssemblyCompanyAttribute)attribs[0]).Company + @"\" + Assembly.GetCallingAssembly().GetName().Name + @"\" + "log";
                }
                else
                {
                    folderName = TAG + @"\" + Assembly.GetCallingAssembly().GetName().Name + @"\" + "log";
                }

                fileName =
                   Assembly.GetCallingAssembly().GetName().Name + "_" +
                   Assembly.GetCallingAssembly().GetName().Version + "_" +
                   dateTimeNow + ".txt";

                directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                directory = directory + @"\" + folderName;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                filePath = directory + @"\" + fileName;

                AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            }
            catch (Exception e)
            {
                try
                {
                    var dateTimeNow = DateTime.Now.ToString("ddMMyyHHmmss");
                    fileName =
                    Assembly.GetCallingAssembly().GetName().Name + "_" +
                    Assembly.GetCallingAssembly().GetName().Version + "_" +
                    dateTimeNow + ".txt";

                    directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data";

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    filePath = directory + @"\" + fileName;

                    AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_ProcessExit;
                    AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                    AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                }
                catch
                {

                }
            }

            DeleteLogInFolder();
        }

        /// <summary>
        /// Delete old logs 
        /// </summary>
        private static void DeleteLogInFolder()
        {
            try
            {
                var enumerateFile = Directory.EnumerateFiles(directory, "*.txt");
                List<string> fileNames = new List<string>(enumerateFile);
                var fileCount = enumerateFile.Count();
                if (fileCount > OLD_LOG_FILES_CAPACITY)
                {
                    for (int i = 0; i < fileCount - OLD_LOG_FILES_CAPACITY; i++)
                    {
                        var temp = fileNames[i];
                        File.Delete(temp);
                    }
                }
            }
            catch (Exception e)
            {

            }

        }

        /// <summary>
        /// When a writting log task was push to a queue, process the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TaskQueueChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Task.Run(() => ProcessQueue());
            }
        }

        /// <summary>
        /// Fire a log when app fall into unhandle exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLog("F", TAG, "[UnhandledException]:" + e.ExceptionObject.ToString());
            ExportLogFile();
            //var task1 = GenerateTask("F", TAG, "[UnhandledException]:" + e.ExceptionObject.ToString());
            //TaskQueue.Enqueue(task1);
            //var task2 = GenerateTask("", "", "", true);
            //TaskQueue.Enqueue(task2);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="className"></param>
        public Logger(string className)
        {
            this.className = className;

            PId = Process.GetCurrentProcess().Id;
            TId = Thread.CurrentThread.ManagedThreadId;
        }

        private static void InitUserLog()
        {
            _userLogBuilder = new StringBuilder();
        }

        private static void InitLogDebug()
        {
            _logBuilder = new StringBuilder();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            WriteLog("I", "App", "User requested exit app!");
            ExportLogFile();
        }

        public void I(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("I", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        public void D(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("D", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        public void E(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("E", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        public void W(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("W", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        public void F(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("F", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        public void V(string message, [CallerMemberName] string callMemberName = null)
        {
            var task = GenerateTask("V", TAG, className, callMemberName, message);
            TaskQueue.Enqueue(task);
        }

        /// <summary>
        /// Process the queue when a task was pushed in
        /// do the task and remove it from queue if it is done
        /// or cancel if it try to do it three times
        /// </summary>
        private static async void ProcessQueue()
        {
            await Mutex.WaitAsync();
            try
            {
                int reDoWorkCounter = 0;

                while (TaskQueue.Count >= 1)
                {
                    var taskFormQueue = TaskQueue.Peek();
                    reDoWorkCounter++;
                    taskFormQueue.Start();
                    var success = taskFormQueue.Result;
                    if (success || reDoWorkCounter >= 3)
                    {
                        var removeTask = TaskQueue.Dequeue();
                        removeTask = null;
                        reDoWorkCounter = 0;
                    }
                }
            }
            finally
            {
                Mutex.Release();
            }
        }

        /// <summary>
        /// Generate a writting log task, to handle write log async
        /// </summary>
        /// <param name="logLV"></param>
        /// <param name="TAG"></param>
        /// <param name="className"></param>
        /// <param name="callMemberName"></param>
        /// <param name="message"></param>
        /// <param name="isExportLogFile"></param>
        /// <returns></returns>
        private Task<bool> GenerateTask(string logLV, string TAG, string className, string callMemberName, string message, bool isExportLogFile = false)
        {
            var task = !isExportLogFile ?
                new Task<bool>(() =>
                {
                    return WriteLog(logLV, TAG, className, callMemberName, message);
                }) :
             new Task<bool>(() =>
             {
                 var resExportLog = ExportLogFile();
                 return resExportLog;
             });

            return task;
        }

        /// <summary>
        /// Generate a writting log task, to handle write log async
        /// </summary>
        /// <param name="logLV"></param>
        /// <param name="TAG"></param>
        /// <param name="className"></param>
        /// <param name="callMemberName"></param>
        /// <param name="message"></param>
        /// <param name="isExportLogFile"></param>
        /// <returns></returns>
        private static Task<bool> GenerateTask(string logLV, string TAG, string message, bool isExportLogFile = false)
        {
            var task = !isExportLogFile ?
                new Task<bool>(() =>
                {
                    return WriteLog(logLV, TAG, message);
                }) :
            new Task<bool>(() =>
            {
                var resExportLog = ExportLogFile();
                return resExportLog;
            });
            return task;
        }

        /// <summary>
        /// Append the message log to log builder
        /// </summary>
        /// <param name="logLv"></param>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool WriteLog(string logLv, string tag, string className, string methodName, string message)
        {
            try
            {
                // Log format
                // (dd-MM HH:mm:ss) (Log lv) (Pid) (Tid) (Tag) (Class name:Method name:Message)
                var dateTimeNow = DateTime.Now.ToString("dd-MM HH:mm:ss:ffffff");
                var newLogLine = dateTimeNow + " " +
                    logLv + " " +
                    PId + " " +
                    TId + " " +
                    tag + " " +
                    className + ":" + methodName + ":" + message;

                if (_logBuilder != null)
                {
                    _logBuilder.AppendLine(newLogLine);
                    ClearBuffer(_logBuilder);
                }

                if (_userLogBuilder != null)
                {
                    switch (logLv)
                    {
                        case "D":
                            break;
                        default:
                            _userLogBuilder.AppendLine(newLogLine);
                            break;
                    }
                    ClearBuffer(_userLogBuilder);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Append the message log to log builder
        /// </summary>
        /// <param name="logLv"></param>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool WriteLog(string logLv, string tag, string message)
        {
            try
            {

                var dateTimeNow = DateTime.Now.ToString("dd-MM HH:mm:ss:ffffff");
                var newLogLine = dateTimeNow + " " +
                logLv + " " +
                tag + " " +
                    message;

                if (_logBuilder != null)
                {
                    _logBuilder.AppendLine(newLogLine);
                    ClearBuffer(_logBuilder);
                }

                if (_userLogBuilder != null)
                {
                    switch (logLv)
                    {
                        case "D":
                            break;
                        default:
                            _userLogBuilder.AppendLine(newLogLine);
                            break;
                    }
                    ClearBuffer(_userLogBuilder);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Clear the builder's buffer if reach max capacity 
        /// </summary>
        /// <param name="builder"></param>
        private static void ClearBuffer(StringBuilder builder)
        {
            if (builder.Capacity >= builder.MaxCapacity - 100000)
            {
                ExportLogFile();
                builder.Clear();
            }
        }

        /// <summary>
        /// Export log from string builder to file .txt
        /// </summary>
        /// <returns></returns>
        public static bool ExportLogFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }

                if (_logBuilder != null)
                {
                    File.AppendAllText(filePath, _logBuilder.ToString());
                }
                else if (_userLogBuilder != null)
                {
                    File.AppendAllText(filePath, _userLogBuilder.ToString());
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// return the directory of Pharmarcy's log files
        /// </summary>
        /// <returns></returns>
        public string GetLogDirectory()
        {
            return directory;
        }
    }

    internal class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public new T Dequeue()
        {
            var x = base.Dequeue();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, x));
            return x;
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            RaiseCollectionChanged(e);
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
    }
}
