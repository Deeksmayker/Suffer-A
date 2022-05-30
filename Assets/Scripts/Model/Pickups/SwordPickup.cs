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
            GlobalEvents.OnSwordPickup.Invoke();

            GameObject.Find("меч").GetComponent<SpriteRenderer>().enabled = true;
            
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Атака X. Если зажать и отжать в нужный момент произведется усиленная атака";
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