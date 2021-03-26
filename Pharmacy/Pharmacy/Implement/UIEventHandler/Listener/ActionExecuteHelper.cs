using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;

namespace Pharmacy.Implement.UIEventHandler.Listener
{
    public class ActionExecuteHelper
    {
        private static ActionExecuteHelper _instance;

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
        private Dictionary<string, Dictionary<string, IAction>> ActionsCache { get; set; }

        /// <summary>
        /// Action cache for Login Screen Window
        /// Currently only support cache for destroyable action
        /// </summary>
        private Dictionary<string, Dictionary<string, IAction>> DestroyableActionsCache;

        private ActionExecuteHelper()
        {
            ActionsCache = new Dictionary<string, Dictionary<string, IAction>>();
            DestroyableActionsCache = new Dictionary<string, Dictionary<string, IAction>>();
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
                        if (cmdEx is IDestroyable)
                        {
                            DestroyableActionsCache[action.BuilderID].Remove(action.ActionID);
                        }
                        else
                        {
                            ActionsCache[action.BuilderID].Remove(action.ActionID);
                        }
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

        public ExecuteStatus ExecuteAction(IAction action, object dataTransfer)
        {
            var provider = action as ICommandExecuter;

            if (provider != null)
            {

                if (!ActionsCache.ContainsKey(action.BuilderID))
                {
                    ActionsCache.Add(action.BuilderID, new Dictionary<string, IAction>());
                }

                if (!DestroyableActionsCache.ContainsKey(action.BuilderID))
                {
                    DestroyableActionsCache.Add(action.BuilderID, new Dictionary<string, IAction>());
                }

                if (!ActionsCache[action.BuilderID].ContainsKey(action.ActionID))
                {
                    if (action is IDestroyable)
                    {
                        DestroyableActionsCache[action.BuilderID].Add(action.ActionID, provider);
                    }
                    else
                    {
                        ActionsCache[action.BuilderID].Add(action.ActionID, provider);
                    }
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

        public IAction GetActionInCache(string builderID, string keyID)
        {
            try
            {
                return DestroyableActionsCache[builderID][keyID];
            }
            catch { }

            try
            {
                return ActionsCache[builderID][keyID];
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
            return CheckInNormalCache(actionID, builderID)
                && CheckInDestroyableCache(actionID, builderID);
        }

        private bool CheckInNormalCache(string actionID, string builderID)
        {
            try
            {
                return !ActionsCache[builderID].ContainsKey(actionID);
            }
            catch
            {
                return true;
            }
        }

        private bool CheckInDestroyableCache(string actionID, string builderID)
        {
            try
            {
                return !DestroyableActionsCache[builderID].ContainsKey(actionID);
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
}
