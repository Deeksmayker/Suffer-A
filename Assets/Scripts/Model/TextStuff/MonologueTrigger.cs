using System;
using DefaultNamespace.Pickups;
using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.TextStuff
{
    public class MonologueTrigger : MonoBehaviour
    {
        public Monologue monologue;
        public static UnityEvent<Monologue> OnMonologueTriggered = new UnityEvent<Monologue>();

        private void Start()
        {
            if (PickupsUseHandler.PickupUsed(gameObject.name))
            {
                Destroy(gameObject);
                return;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null || PickupsUseHandler.PickupUsed(gameObject.name))
                return;
            
            OnMonologueTriggered.Invoke(monologue);
            PickupsUseHandler.RememberPickup(gameObject.name);
        }
    }
}