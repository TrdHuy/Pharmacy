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

        /// <summary>
        /// The capacity of action executer
        /// For each builder, there is 10 slots
        /// For the cache, there is 10 slots for builder
        /// ==> Total slots: 100
        /// So the app can handle 100 seprated action in the same time
        /// </summary>
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


        /// <summary>
        /// The cache storage action by seprated key, and each action
        /// belong to sepreated builder. The builded is specified by key.
        /// </summary>
        private Dictionary<string, Dictionary<string, IAction>> ExecutingActionsCache { get; set; }


        private ActionExecuteHelper()
        {
            ExecutingActionsCache = new Dictionary<string, Dictionary<string, IAction>>(MAX_BUILDER_CAPACITY);
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

        /// <summary>
        /// Update the action cache
        /// Check for the action is completed or cancled or not
        /// if it was cancled or completed, unregister it from the cache
        /// </summary>
        /// <param name="action"></param>
        private void HelperUpdate(IAction action)
        {
            if (action is ICommandExecuter)
            {
                var cmdEx = action as ICommandExecuter;

                if (cmdEx.IsCompleted || cmdEx.IsCanceled)
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

        /// <summary>
        /// Register an action to cache and then execute it
        /// If the action is already existed in the cache, it will be executed alternatively
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dataTransfer"></param>
        /// <returns></returns>
        public ExecuteStatus ExecuteAction(IAction action, object dataTransfer)
        {
            var provider = action as ICommandExecuter;

            if (provider != null)
            {
                if (ExecutingActionsCache.Count > MAX_BUILDER_CAPACITY)
                {
                    throw new InvalidOperationException("Capacity of builder now is maximum!");
                }

                if (!ExecutingActionsCache.ContainsKey(action.BuilderID))
                {
                    var actionCache = new Dictionary<string, IAction>(MAX_ACTION_CAPACITY_EACH_BUILDER);
                    ExecutingActionsCache.Add(action.BuilderID, actionCache);
                }

                if (!ExecutingActionsCache[action.BuilderID].ContainsKey(action.ActionID))
                {
                    if (ExecutingActionsCache[action.BuilderID].Count > MAX_ACTION_CAPACITY_EACH_BUILDER)
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
                    provider?.AlterExecute(dataTransfer);
                    return ExecuteStatus.ExistedExecuter;
                }
            }

            return ExecuteStatus.None;
        }

        private void RegisterActionToCache(IAction action)
        {
            try
            {
                ExecutingActionsCache[action.BuilderID].Add(action.ActionID, action);
                UpdateHelperStatus();

                if (action.Logger != null)
                {
                    action.Logger.I($"Registered successfully action: keyID = {action.ActionID}, builderID = {action.BuilderID}");
                }
            }
            catch (Exception e)
            {
                if (action.Logger != null)
                {
                    action.Logger.E(e.Message);
                }
            }
        }

        private void UnregisterActionToCache(IAction action)
        {
            try
            {
                ExecutingActionsCache[action.BuilderID].Remove(action.ActionID);
                UpdateHelperStatus();
                if (action.Logger != null)
                {
                    action.Logger.I($"Unregistered successfully action: keyID = {action.ActionID}, builderID = {action.BuilderID}");
                }
            }
            catch (Exception e)
            {
                if (action.Logger != null)
                {
                    action.Logger.E(e.Message);
                }
            }
        }

        /// <summary>
        /// Update the ActionExecuteHelper status
        /// if all builder are free means the is no any action in the cache, it will return Status = Available
        /// else some of builder are remain some executing actions, it will return status = RemainSomeExecutingActions
        /// and if all builder all full of capacity for cache, it will return status = unavailable
        /// </summary>
        private void UpdateHelperStatus()
        {
            bool isAllBuilderAreFree = false;
            foreach (var key in ExecutingActionsCache.Keys)
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
                foreach (var key in ExecutingActionsCache.Keys)
                {
                    isAllBuilderAreFullCache = ExecutingActionsCache[key].Count == MAX_ACTION_CAPACITY_EACH_BUILDER;
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
                return ExecutingActionsCache[builderID][keyID];
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
                return !ExecutingActionsCache[builderID].ContainsKey(actionID);
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
                return !(ExecutingActionsCache[builderID].Count > 0);
            }
            catch
            {
                return true;
            }
        }

        public void CancelAllAction()
        {
            foreach (var actions in ExecutingActionsCache.Values)
            {
                foreach (var action in actions.Values)
                {
                    if (action is ICommandExecuter)
                    {
                        ((ICommandExecuter)action).OnCancel();
                    }
                }
            }
        }

        public void CancelActionWithFeatureKey(string actionKey)
        {
            try
            {
                foreach (var actions in ExecutingActionsCache.Values)
                {
                    foreach (var act in actions.Values)
                    {
                        if (act is ICommandExecuter && act.ActionID.Equals(actionKey))
                        {
                            ((ICommandExecuter)act).OnCancel();
                            return;
                        }
                    }
                }
            }
            catch
            {

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


