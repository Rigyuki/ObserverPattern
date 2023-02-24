namespace Patterns.ObserverPattern
{
    public class MsgCenterByDelegate
    {
        public static bool Enabled = false;
        
        private static System.Action<ColorType> _ColorTypeAction;

        public static void AddListener(System.Action<ColorType> action)
        {
            _ColorTypeAction += action;
        }

        public static void RemoveListener(System.Action<ColorType> action)
        {
            _ColorTypeAction -= action;
        }

        public static void SendMsg(ColorType msg)
        {
            if (!Enabled)
            {
                return;
            }
            _ColorTypeAction?.Invoke(msg);
        }
    }
}