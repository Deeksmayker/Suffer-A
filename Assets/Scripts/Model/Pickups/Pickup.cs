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
        public GameObject infoPanel;
        public ParticleSystem particles;
        protected ParticleSystem ParticleInstance;
        protected bool AlreadyUsed;

        private void Start()
        {
            ParticleInstance = Instantiate(particles, transform.position, Quaternion.identity);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null || AlreadyUsed)
                return;
            
            infoPanel.SetActive(true);
            infoPanel.GetComponentInChildren<Text>().text = "↑";
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null || AlreadyUsed)
                return;
            
            if (Input.GetKey(PlayerInGameInput.InteractionKey))
                Interact();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;
            
            if (infoPanel.GetComponentInChildren<Text>().text == "↑")
                infoPanel.SetActive(false);
        }

        public virtual void Interact()
        {
            AlreadyUsed = true;
        }

        public abstract IEnumerator ShowInfoPanel();
    }
}