using UnityEngine;

namespace DefaultNamespace.Fight
{
    public abstract class Entity : MonoBehaviour
    {
        public int Health { get; protected set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Die();
        }

        public void Die()
        {
            GlobalEvents.OnEnemyDeath.Invoke(transform);
            Destroy(transform);
        }
    }
}