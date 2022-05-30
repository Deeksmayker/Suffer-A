using System;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class FallingSpikeTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D spikeRb;
        [SerializeField] private AudioSource fallSound;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null)
                return;
            Instantiate(fallSound, transform);
            spikeRb.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            
        }
    }
}