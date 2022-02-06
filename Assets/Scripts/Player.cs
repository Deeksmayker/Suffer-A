namespace DefaultNamespace
{
    public class Player
    {
        public static bool ControlEnabled { get; private set; } = true;
        public static int LungeAirCount { get; private set; } = 1;

        public static void EnableControl()
        {
            ControlEnabled = true;
        }
        
        public static void DisableControl()
        {
            ControlEnabled = false;
        }

        public static void SetAirLungeCount(int value)
        {
            LungeAirCount = value;
        }
    }
}