using System;
using DefaultNamespace.Fight;
using Movement;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Platformer
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] private ParticleSystem destroyParticles;
        [SerializeField] private AudioSource destroySound;
        
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerHealth>() != null)
                PlayerHealth.OnHitTaken.Invoke(1);
            
            Instantiate(destroyParticles, transform.position, quaternion.identity);
            Instantiate(destroySound, transform);
            Destroy(gameObject);
        }
    }
}