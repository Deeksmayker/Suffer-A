using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class SwordPickup : Pickup
    {
        public override void Interact()
        {
            PlayerPreferences.AttackAvailable = true;
            StartCoroutine(ShowInfoPanel());
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Атака X. Если зажать и отжать в нужный момент произведется усиленная атака";
            infoPanel.SetActive(true);
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.SetActive(false);
            Destroy(gameObject);

        }
    }
}