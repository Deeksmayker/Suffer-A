using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPreferences
    {
        public static Vector3 SpawnPoint { get; private set; }
        public static bool ControlEnabled { get; private set; } = true;
        public static int MaxLungeAirCount { get; private set; } = 1;
        
        public const float CoyoteTime = 0.05f;
        public const float InAirCoyoteTime = 0.17f;

        public static bool IsGrounded;
        public static bool FaceRight = true;

        public static int Damage { get; private set; } = 4;

        public static void EnableControl()
        {
            ControlEnabled = true;
        }
        
        public static void DisableControl()
        {
            ControlEnabled = false;
        }

        public static void SetMaxAirLungeCount(int value)
        {
            MaxLungeAirCount = value;
        }

        public static void SetSpawnPoint(Vector3 point)
        {
            SpawnPoint = point;
        }
    }
}