using System.Collections.Generic;

namespace DefaultNamespace.Pickups
{
    public class PickupsUseHandler
    {
        private static List<string> _usedPickups = new List<string>();

        public static void RememberPickup(string name)
        {
            _usedPickups.Add(name);
        }

        public static bool PickupUsed(string name)
        {
            return _usedPickups.Contains(name);
        }
    }
}