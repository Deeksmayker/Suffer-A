using System;
using System.Collections;
using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Pickup : MonoBehaviour
    {
        protected GameObject infoPanel;
        public ParticleSystem particles;
        protected ParticleSystem ParticleInstance;

        private void Start()
        {
            if (PickupsUseHandler.PickupUsed(gameObject.name))
            {
                Destroy(gameObject);
                return;
            }
            
            infoPanel = GameObject.FindWithTag("Info");
            ParticleInstance = Instantiate(particles, transform.position, Quaternion.identity);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null || PickupsUseHandler.PickupUsed(gameObject.name))
                return;

            infoPanel.GetComponent<Image>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().text = "↑";
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null || PickupsUseHandler.PickupUsed(gameObject.name))
                return;
            
            if (Input.GetKey(PlayerInGameInput.InteractionKey))
                Interact();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;

            if (infoPanel.GetComponentInChildren<Text>().text == "↑")
            {
                infoPanel.GetComponent<Image>().enabled = false;
                infoPanel.GetComponentInChildren<Text>().enabled = false;
            }
        }

        public virtual void Interact()
        {
            PickupsUseHandler.RememberPickup(gameObject.name);
            GlobalEvents.OnPickup.Invoke();
        }

        public abstract IEnumerator ShowInfoPanel();
    }
}