using System;
using DefaultNamespace.Pickups;
using Lean.Localization;
using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.TextStuff
{
    public class MonologueTrigger : MonoBehaviour
    {
        public LeanPhrase monologue;
        public static UnityEvent<Monologue> OnMonologueTriggered = new UnityEvent<Monologue>();

        [SerializeField] private bool isNotDisappear;

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

            var newMonologue = new Monologue();
            newMonologue.sentences = new[]
            {
                monologue.Entries
                    .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text
            };

            OnMonologueTriggered.Invoke(newMonologue);

            if (isNotDisappear)
                return;
            PickupsUseHandler.RememberPickup(gameObject.name);
        }
    }
}