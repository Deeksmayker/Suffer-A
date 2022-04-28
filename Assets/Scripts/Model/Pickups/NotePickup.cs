using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class NotePickup : Pickup
    {
        [SerializeField] private GameObject noteButton;
    
        public override void Interact()
        {
            noteButton.SetActive(true);
            StartCoroutine(ShowInfoPanel());
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Поднята записка";
            infoPanel.SetActive(true);
            ParticleInstance.Stop();
            yield return new WaitForSeconds(2f);
            infoPanel.SetActive(false);
            Destroy(gameObject);
        }
    }
}