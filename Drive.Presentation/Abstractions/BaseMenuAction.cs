﻿using Internship_7_Drive.Extensions;

namespace Internship_7_Drive.Abstractions
{
    public class BaseMenuAction : IMenuAction
    {
        public int MenuIndex { get; set; }
        public string Name { get; set; } = "NoName action";
        public IList<IAction> Actions { get; set; }

        public BaseMenuAction(IList<IAction> actions)
        {
            actions.SetActionIndexes();
            Actions = actions;
        }

        public virtual void Open()
        {
            Actions.PrintActionsAndOpen();
        }
    }
}
