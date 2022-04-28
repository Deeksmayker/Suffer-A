using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class DownAbilityPickup : Pickup
    {
        public override void Interact()
        {
            PlayerPreferences.DownAbilityAvailable = true;
            StartCoroutine(ShowInfoPanel());
            base.Interact();

        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "↓ + F";
            infoPanel.SetActive(true);
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.SetActive(false);
            Destroy(gameObject);

        }
    }
}