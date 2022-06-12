using System.Collections;
using Lean.Localization;
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

        [SerializeField] private LeanPhrase airLungePhrase;
        
        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponent<Image>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().enabled = true;
            infoPanel.GetComponentInChildren<Text>().text = airLungePhrase.Entries
                .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text;
            
            ParticleInstance.Stop();

            yield return new WaitForSeconds(5f);
            infoPanel.GetComponent<Image>().enabled = false;
            infoPanel.GetComponentInChildren<Text>().enabled = false;
            Destroy(gameObject);

        }
    }
}