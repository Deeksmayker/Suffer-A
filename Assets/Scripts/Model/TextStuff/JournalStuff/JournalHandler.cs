using System;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TextStuff.JournalStuff
{
    public class JournalHandler : MonoBehaviour
    {
        [SerializeField] private List<LeanPhrase> _secondDescriptions = new List<LeanPhrase>();
        
        public static Dictionary<string, int> EnemiesKillCount = new Dictionary<string, int>
        {
            {"Stone", 0},
            {"Head", 0},
            {"Father", 0},
            {"Bat", 0},
            {"Wall", 0},
            {"Pango", 0}
        };
        
        private List<int> _openedButtons= new List<int>();
        private List<int> _openedFullDescription = new List<int>();
        
        [SerializeField] private List<GameObject> journalButtons;

        [SerializeField] private GameObject secondDescription;

        public LeanPhrase textOnNewJournalNote;

        private void Update()
        {
            int index = 0;
            foreach (var enemies in EnemiesKillCount)
            {
                if (enemies.Value >= 1 && !_openedButtons.Contains(index))
                {
                    var newMonologue = new Monologue();
                    newMonologue.sentences = new []{
                        textOnNewJournalNote.Entries
                                .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage())
                                .Text
                    };
                    
                    MonologueTrigger.OnMonologueTriggered.Invoke(newMonologue);
                    journalButtons[index].SetActive(true);
                    _openedButtons.Add(index);
                }

                if (enemies.Value >= 5 && !_openedFullDescription.Contains(index))
                {
                    var newMonologue = new Monologue();
                    newMonologue.sentences = new []{
                        textOnNewJournalNote.Entries
                            .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage())
                            .Text
                    };
                    
                    MonologueTrigger.OnMonologueTriggered.Invoke(newMonologue);
                    var description = _secondDescriptions[index].Entries
                        .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text;;
                    journalButtons[index].GetComponent<Button>().onClick.AddListener(() => secondDescription.GetComponent<Text>().text = description);
                    _openedFullDescription.Add(index);
                }

                index++;
            }
                
        }
    }
}