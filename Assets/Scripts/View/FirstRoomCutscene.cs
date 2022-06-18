using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class FirstRoomCutscene : MonoBehaviour
    {
        public static UnityEvent OnPlayBegin = new UnityEvent();
        
        private void Start()
        {
            PlayerPreferences.CurrentHealth = PlayerPreferences.MaxHealth;
            PlayerPreferences.CurrentBlood = PlayerPreferences.MaxBlood;
            PlayerPreferences.FaceRight = true;
            PlayerPreferences.AttackAvailable = false;
            PlayerPreferences.CanMove = true;
            PlayerPreferences.DownAbilityAvailable = false;
            PlayerPreferences.HorizontalAbilityAvailable = false;
            PlayerPreferences.UpAbilityAvailable = false;
            PlayerPreferences.MaxLungeAirCount = 0;
            
            OnPlayBegin.Invoke();
        }
    }
}