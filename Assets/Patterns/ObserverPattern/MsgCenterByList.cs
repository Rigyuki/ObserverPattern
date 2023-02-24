using System;
using System.Collections.Generic;

namespace Patterns.ObserverPattern
{
    public class MsgCenterByList
    {
        public const int MSG_COLOR_BUTTON_CLICKED = 1;
        
        public static bool Enabled = true;
        private static List<System.Action<CommonMsg>> _actions = new List<Action<CommonMsg>>(1024);

        public static void AddListener(System.Action<CommonMsg> action)
        {
            if (!_actions.Contains(action))
            {
                _actions.Add(action);
            }
        }

        public static void RemoveListener(System.Action<CommonMsg> action)
        {
            _actions.Remove(action);
        }

        public static void SendMessage(CommonMsg commonMsg)
        {
            if (!Enabled)
            {
                return;
            }
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                if (_actions[i] != null)
                {
                    _actions[i].Invoke(commonMsg);
                }
            }
        }
    

}
}