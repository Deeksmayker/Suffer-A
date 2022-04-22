using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPreferences
    {
        public static int MaxHealth = 4;
        private static int _currentHealth = MaxHealth;
        public static int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }

        public static float MaxBlood = 100;

        private static float _currentBlood = MaxBlood;
        public static float CurrentBlood
        {
            get => _currentBlood;
            set => _currentBlood = Mathf.Clamp(value, 0, MaxBlood);
        }
        public static float BloodSpend = 50;
        
        public static Vector3 SpawnPoint { get; private set; }

        public static bool InTransition = false;
        public static bool ControlEnabled { get; private set; } = true;
        public static bool CanTakeDamage = true;
        public static bool CanMove = true;
        public static int MaxLungeAirCount { get; private set; } = 1;
        
        public const float CoyoteTime = 0.05f;
        public const float InAirCoyoteTime = 0.17f;

        public static bool IsGrounded;
        public static bool FaceRight = true;
        public static int LookDirection => FaceRight ? 1 : -1;

        public static int HitDamage { get; private set; } = 1;
        public static int HorizontalProjectileDamage = 4;

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