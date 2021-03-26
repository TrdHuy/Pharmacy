using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class ActionExecuteHelper
    {
        private static ActionExecuteHelper _instance;
        private const int MAX_BUILDER_CAPACITY = 10;
        private const int MAX_ACTION_CAPACITY_EACH_BUILDER = 10;
        public static ActionExecuteHelper Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ActionExecuteHelper();
                }
                return _instance;
            }
        }

        public HelperStatus Status { get; private set; }

        private Dictionary<string, Dictionary<string, IAction>> BuildersCache { get; set; }


        private ActionExecuteHelper()
        {
            BuildersCache = new Dictionary<string, Dictionary<string, IAction>>(MAX_BUILDER_CAPACITY);
            Status = HelperStatus.Available;
        }


        private void ActionIsCompletedChanged(object sender, ExecuterStatusArgs arg)
        {
            HelperUpdate(sender as IAction);
        }

        private void ActionIsCanceledChanged(object sender, ExecuterStatusArgs arg)
        {
            HelperUpdate(sender as IAction);
        }

        private void HelperUpdate(IAction action)
        {
            if (action is ICommandExecuter)
            {
                var cmdEx = action as ICommandExecuter;

                if (cmdEx.IsCompleted || cmdEx.IsCancled)
                {
                    try
                    {
                        UnregisterActionToCache(action);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        cmdEx.IsCanceledChanged -= ActionIsCanceledChanged;
                        cmdEx.IsCompletedChanged -= ActionIsCompletedChanged;
                    }
                }
            }
        }

        public ExecuteStatus ExecuteAlterAction(IAction action, object dataTransfer)
        {
            var provider = action as ICommandExecuter;

            if (provider != null)
            {

                if (!BuildersCache.ContainsKey(action.BuilderID))
                {
                    var actionCache = new Dictionary<string, IAction>(MAX_ACTION_CAPACITY_EACH_BUILDER);
                    BuildersCache.Add(action.BuilderID, actionCache);
                }

                if (!BuildersCache[action.BuilderID].ContainsKey(action.ActionID))
                {
                    RegisterActionToCache(provider);

                    provider.IsCompletedChanged += ActionIsCompletedChanged;
                    provider.IsCanceledChanged += ActionIsCanceledChanged;

                    provider?.AlterExecute(dataTransfer);

                    return ExecuteStatus.OK;
                }
                else
                {
                    return ExecuteStatus.ExistedExecuter;
                }
            }

            return ExecuteStatus.None;
        }

        public ExecuteStatus ExecuteAction(IAction action, object dataTransfer)
        {
            var provider = action as ICommandExecuter;

            if (provider != null)
            {
                if (BuildersCache.Count > MAX_BUILDER_CAPACITY)
                {
                    throw new InvalidOperationException("Capacity of builder now is maximum!");
                }

                if (!BuildersCache.ContainsKey(action.BuilderID))
                {
                    var actionCache = new Dictionary<string, IAction>(MAX_ACTION_CAPACITY_EACH_BUILDER);
                    BuildersCache.Add(action.BuilderID, actionCache);
                }

                if (!BuildersCache[action.BuilderID].ContainsKey(action.ActionID))
                {
                    if (BuildersCache[action.BuilderID].Count > MAX_ACTION_CAPACITY_EACH_BUILDER)
                    {
                        throw new InvalidOperationException("Capacity of action now is maximum!");
                    }

                    RegisterActionToCache(provider);

                    provider.IsCompletedChanged += ActionIsCompletedChanged;
                    provider.IsCanceledChanged += ActionIsCanceledChanged;

                    provider?.Execute(dataTransfer);

                    return ExecuteStatus.OK;
                }
                else
                {
                    return ExecuteStatus.ExistedExecuter;
                }
            }

            return ExecuteStatus.None;
        }

        private void RegisterActionToCache(IAction action)
        {
            try
            {
                BuildersCache[action.BuilderID].Add(action.ActionID, action);
                UpdateHelperStatus();
            }
            catch { }
        }

        private void UnregisterActionToCache(IAction action)
        {
            try
            {
                BuildersCache[action.BuilderID].Remove(action.ActionID);
                UpdateHelperStatus();
            }
            catch { }
        }

        private void UpdateHelperStatus()
        {
            bool isAllBuilderAreFree = false;
            foreach (var key in BuildersCache.Keys)
            {
                isAllBuilderAreFree = IsAllBuilderActionsFinished(key);
            }
            if (isAllBuilderAreFree)
            {
                Status = HelperStatus.Available;
            }
            else
            {
                bool isAllBuilderAreFullCache = true;
                foreach (var key in BuildersCache.Keys)
                {
                    isAllBuilderAreFullCache = BuildersCache[key].Count == MAX_ACTION_CAPACITY_EACH_BUILDER;
                }

                if (isAllBuilderAreFullCache)
                {
                    Status = HelperStatus.Unavailable;
                }
                else
                {
                    Status = HelperStatus.RemainSomeExecutingActions;
                }
            }
        }

        public IAction GetActionInCache(string builderID, string keyID)
        {
            try
            {
                return BuildersCache[builderID][keyID];
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Check a specific action is finished (included completed case and canceled case)
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="builderID"></param>
        /// <returns></returns>
        public bool IsActionFinished(string actionID, string builderID)
        {
            try
            {
                return !BuildersCache[builderID].ContainsKey(actionID);
            }
            catch
            {
                return true;
            }
        }

        public bool IsAllBuilderActionsFinished(string builderID)
        {
            try
            {
                return !(BuildersCache[builderID].Count > 0);
            }
            catch
            {
                return true;
            }
        }

    }
    public enum ExecuteStatus
    {
        None = 1,

        OK = 2,

        ExistedExecuter = 3,
    }

    public enum HelperStatus
    {
        None = 1,

        Available = 2,

        Unavailable = 3,

        RemainSomeExecutingActions = 4,
    }
}


