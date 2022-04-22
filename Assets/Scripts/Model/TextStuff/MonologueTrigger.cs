using System;
using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.TextStuff
{
    public class MonologueTrigger : MonoBehaviour
    {
        private bool _alreadyTriggered;
        public Monologue monologue;
        public static UnityEvent<Monologue> OnMonologueTriggered = new UnityEvent<Monologue>();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null || _alreadyTriggered)
                return;
            
            OnMonologueTriggered.Invoke(monologue);
            _alreadyTriggered = true;
        }
    }
}