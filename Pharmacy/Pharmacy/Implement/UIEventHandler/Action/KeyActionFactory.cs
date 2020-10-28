﻿using Pharmacy.Base.UIEventHandler.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.UIEventHandler.Action
{
    abstract class KeyActionFactory : IActionFactory
    {
        private static KeyActionFactory _instance;

        public IAction CreateAction(object obj)
        {
            IAction action;
            string keyTag = (string)obj;

            action = CreateActionFromCurrentWindow(keyTag);

            return action;
        }

        public abstract IAction CreateActionFromCurrentWindow(string keyTag);

    }
}
