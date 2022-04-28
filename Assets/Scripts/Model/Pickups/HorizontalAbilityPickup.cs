using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class HorizontalAbilityPickup : Pickup
    {
        public override void Interact()
        {
            PlayerPreferences.HorizontalAbilityAvailable = true;
            StartCoroutine(ShowInfoPanel());
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Способность. В сторону и F";
            infoPanel.SetActive(true);
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.SetActive(false);
            Destroy(gameObject);

        }
    }
}