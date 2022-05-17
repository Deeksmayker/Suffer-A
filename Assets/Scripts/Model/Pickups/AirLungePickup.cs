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
            infoPanel.GetComponent<Image>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().text = "Рывок в воздухе. С";
            
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.GetComponent<Image>().enabled = false;
            infoPanel.GetComponentInChildren<Text>().enabled = false;
            Destroy(gameObject);

        }
    }
}