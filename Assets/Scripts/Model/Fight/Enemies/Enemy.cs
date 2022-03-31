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
        
        [SerializeField] protected int health;
        [SerializeField] protected int damage = 1;
        [SerializeField] protected float speed;
        private SpriteRenderer _sprite;

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null || !PlayerPreferences.CanTakeDamage)
                return;
            
            GlobalEvents.OnPlayerDamaged.Invoke(damage);
        }

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.material.color = Color.white;
        }

        public virtual void TakeDamage(int dmg)
        {
            OnEnemyDamaged.Invoke(gameObject);
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