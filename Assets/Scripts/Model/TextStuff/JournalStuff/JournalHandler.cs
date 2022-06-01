using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TextStuff.JournalStuff
{
    public class JournalHandler : MonoBehaviour
    {
        private List<string> _secondDescriptions = new List<string>()
        {
            "Забавно, но этот камень напоминает мне все те проблемы, которые у меня обнаружили врачи после того дня. Такие вроде-бы маленькие, незаметные, но в самый неприятный момент помешают так сильно, что встать будет трудно.",
            "Кое-кого оно мне напоминает. Наш классный руководитель в…. в школе…. был человеком злопамятным. Еще в начале средних классов мы подшутили над ним…. детская шалость, ничего более, но уроки его после этого превратились в форменное мучение. Он был абсолютно слеп ко всем нашим достижениям, стараниям. Не раз у меня в голове миновала мысль - чтоб ты сдох…. Но в тот день он задержал их…. он подарил нам всем шанс…. ",
            "Я пытался вырезать из своей памяти отца, но воспоминания о нём останутся со мной навсегда. В тот день отец даже не приехал забрать меня. Когда же я наконец вернулся он был пьян и лежал на диване. Он позволял себе много лишнего в отношении меня и матери. Тогда я не мог дать ему отпор.",
            "Это чувство отчетливо напоминает мне о том состоянии непрекращающейся паранойи, которое постоянно преследовало меня после того дня.",
            "Многие дурные мысли и настроения вросли в подкорку моего сознания. Как клещ они зацепились за меня. Бежать от этого не выход. В конце концов как можно убежать от того что засело у тебя в голове. Остается только справится с ними здесь, а иначе зачем я тут.",
            "Моя мать не была плохим человеком, просто она меня никогда не любила."
        };
        
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

        public Monologue textOnNewJournalNote;

        private void Update()
        {
            int index = 0;
            foreach (var enemies in EnemiesKillCount)
            {
                if (enemies.Value >= 1 && !_openedButtons.Contains(index))  
                {
                    MonologueTrigger.OnMonologueTriggered.Invoke(textOnNewJournalNote);
                    journalButtons[index].SetActive(true);
                    _openedButtons.Add(index);
                }

                if (enemies.Value >= 5 && !_openedFullDescription.Contains(index))
                {
                    MonologueTrigger.OnMonologueTriggered.Invoke(textOnNewJournalNote);
                    var description = _secondDescriptions[index];
                    journalButtons[index].GetComponent<Button>().onClick.AddListener(() => secondDescription.GetComponent<Text>().text = description);
                    _openedFullDescription.Add(index);
                }

                index++;
            }
                
        }
    }
}