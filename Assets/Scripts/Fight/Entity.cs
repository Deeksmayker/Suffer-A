using UnityEngine;

namespace DefaultNamespace.Fight
{
    public abstract class Entity : MonoBehaviour
    {
        public int Health { get; protected set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            GlobalEvents.OnEnemyDeath.Invoke(transform);
        }
    }
}