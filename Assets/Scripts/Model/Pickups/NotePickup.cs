using System.Collections;
using DefaultNamespace.TextStuff.JournalStuff;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Pickups
{
    public class NotePickup : Pickup
    {
        [SerializeField] private int noteIndex;
    
        public override void Interact()
        {
            Diary.OnNotePickup.Invoke(noteIndex);
            StartCoroutine(ShowInfoPanel());
            base.Interact();
        }

        public override IEnumerator ShowInfoPanel()
        {
            infoPanel.GetComponentInChildren<Text>().text = "Поднята записка";
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