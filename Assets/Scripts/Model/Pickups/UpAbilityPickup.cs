using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class UpAbilityPickup : Pickup
    {
        public override void Interact()
        {
            PlayerPreferences.UpAbilityAvailable = true;
            StartCoroutine(ShowInfoPanel());
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "↑ + F";
            infoPanel.GetComponent<Image>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().enabled = true;
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.GetComponent<Image>().enabled = false;
            infoPanel.GetComponentInChildren<Text>().enabled = false;
            Destroy(gameObject);

        }
    }
}