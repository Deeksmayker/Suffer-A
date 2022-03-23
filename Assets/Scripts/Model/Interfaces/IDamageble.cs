using UnityEngine;

namespace DefaultNamespace.Fight
{
    public interface IDamageble
    {
        public void TakeDamage(int dmg);
        public void Die();
    }
}