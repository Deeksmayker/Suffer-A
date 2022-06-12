using System.Collections;
using DefaultNamespace.TextStuff.JournalStuff;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class NotePickup : Pickup
    {
        [SerializeField] private int noteIndex;
        [SerializeField] private AudioSource pickSound;
    
        public override void Interact()
        {
            Instantiate(pickSound, transform);
            Diary.OnNotePickup.Invoke(noteIndex);
            StartCoroutine(ShowInfoPanel());
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
            yield return new WaitForSeconds(2f);
            infoPanel.GetComponent<Image>().enabled = false;
            infoPanel.GetComponentInChildren<Text>().enabled = false;
            Destroy(gameObject);
        }
    }
}