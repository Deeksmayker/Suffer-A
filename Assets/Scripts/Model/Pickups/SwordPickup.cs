using System.Collections;
using Lean.Localization;
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

        [SerializeField] private LeanPhrase phrase;
        
        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = phrase.Entries
                .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text;
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