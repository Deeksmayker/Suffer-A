using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.TextStuff.JournalStuff
{
    public class JournalHandler : MonoBehaviour
    {
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
        [SerializeField] private List<GameObject> journalButtons;

        private void Update()
        {
            int index = 0;
            foreach (var enemies in EnemiesKillCount)
            {
                if (enemies.Value >= 5 && !_openedButtons.Contains(index))  
                {
                    journalButtons[index].SetActive(true);
                }

                index++;
            }
                
        }
    }
}