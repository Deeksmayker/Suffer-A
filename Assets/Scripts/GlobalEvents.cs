using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GlobalEvents
    {
        public static UnityEvent OnPlayerDeath = new UnityEvent();
        public static UnityEvent<int> OnPlayerDamaged = new UnityEvent<int>();
        public static UnityEvent<Transform> OnEnemyDeath = new UnityEvent<Transform>();
    }
}