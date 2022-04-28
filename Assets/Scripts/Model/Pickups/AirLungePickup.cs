using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class AirLungePickup : Pickup
    {
        public override void Interact()
        {
            PlayerPreferences.MaxLungeAirCount = 1;
            StartCoroutine(ShowInfoPanel());
            base.Interact();

        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Рывок в воздухе. С";
            infoPanel.SetActive(true);
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.SetActive(false);
            Destroy(gameObject);

        }
    }
}