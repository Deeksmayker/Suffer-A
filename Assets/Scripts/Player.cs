namespace DefaultNamespace
{
    public class Player
    {
        public static bool ControlEnabled { get; private set; } = true;

        public static void EnableControl()
        {
            ControlEnabled = true;
        }
        
        public static void DisableControl()
        {
            ControlEnabled = false;
        }
    }
}