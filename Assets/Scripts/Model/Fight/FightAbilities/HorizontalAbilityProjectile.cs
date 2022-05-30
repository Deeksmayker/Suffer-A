using System;
using Movement;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public class HorizontalAbilityProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem flyingParticle;
        [SerializeField] private ParticleSystem destroyParticle;

        private ParticleSystem _flyingParticles;

        private void Awake()
        {
            _flyingParticles = Instantiate(flyingParticle, transform.position, quaternion.identity);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() != null)
                return;
            
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Enemy>() == null || col.gameObject.GetComponent<PlayerController>() != null)
                return;
            
            col.GetComponent<Enemy>().TakeDamage(PlayerPreferences.HorizontalProjectileDamage);

            Destroy(gameObject);
        }

        private void Update()
        {
            _flyingParticles.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            Destroy(_flyingParticles);
            Instantiate(destroyParticle, transform.position, quaternion.identity);
        }
    }
}