using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.TextStuff.JournalStuff
{
    public class Diary : MonoBehaviour
    {
        [SerializeField] private GameObject[] diaryButtons;

        public static UnityEvent<int> OnNotePickup = new UnityEvent<int>();

        private void Awake()
        {
            OnNotePickup.AddListener(ShowButton);
        }

        private void ShowButton(int index)
        {
            diaryButtons[index - 1].SetActive(true);
        }
    }
}