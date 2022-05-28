using System;
using Mechanics;
using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class Enemy : MonoBehaviour, IDamageble
    {
        public static UnityEvent<GameObject> OnEnemyDamaged = new UnityEvent<GameObject>();
        public static UnityEvent<GameObject> OnEnemyPowerDamaged = new UnityEvent<GameObject>();
        public static UnityEvent<GameObject> OnProjectileDamaged = new UnityEvent<GameObject>();
        
        [SerializeField] public int health;
        [SerializeField] public int damage = 1;

        [SerializeField] protected ParticleSystem hitParticles;
        
        private SpriteRenderer _sprite;

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null || !PlayerPreferences.CanTakeDamage)
                return;
            
            if (damage != 0)
                PlayerHealth.OnHitTaken.Invoke(damage);
        }

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            if (_sprite != null)
                _sprite.material.color = Color.white;
        }

        public virtual void TakeDamage(int dmg)
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            OnEnemyDamaged.Invoke(gameObject);
            if (dmg > PlayerPreferences.HitDamage)
                OnEnemyPowerDamaged.Invoke(gameObject);
            else
                OnProjectileDamaged.Invoke(gameObject);
            health -= dmg;
            if (health <= 0)
                Die();
        }

        public virtual void DealDamage()
        {
            
        }

        public virtual void Die()
        {
            GlobalEvents.OnEnemyDeath.Invoke(transform);
            Destroy(gameObject);
        }
    }
}