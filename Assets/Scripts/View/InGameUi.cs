using System;
using System.Collections;
using DefaultNamespace.Fight;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private Slider bloodSlider;
        [SerializeField] private Slider healthSlider;

        [SerializeField] private GameObject journal;
        [SerializeField] private GameObject diary;
        [SerializeField] private GameObject pause;

        public static UnityEvent<float> OnBloodIncrease = new UnityEvent<float>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            bloodSlider.value = PlayerPreferences.CurrentBlood;
            healthSlider.value = PlayerPreferences.CurrentHealth;
            PlayerHealth.OnDamageTaken.AddListener(DecreaseHealthValue);
            PlayerHealth.OnHeal.AddListener(IncreaseHealthValue);
            PlayerHealth.OnHeal.AddListener(DecreaseBloodValue);
            PlayerInGameInput.OnAbility.AddListener(DecreaseBloodValue);
            PlayerAttack.OnHit.AddListener(IncreaseBloodValue);
            OnBloodIncrease.AddListener(IncreaseBloodValue);
            
            PlayerHealth.OnDie.AddListener(() => StartCoroutine(SetMaxHealthAndSetBlood()));
        }

        private void Update()
        {
            CheckMenuInput();
            CheckMenuCloseInput();
        }

        private IEnumerator SetMaxHealthAndSetBlood()
        {
            yield return new WaitForSeconds(PlayerPreferences.RespawnTime);
            healthSlider.value = PlayerPreferences.MaxHealth;
            bloodSlider.value = 0;
        }

        private void DecreaseHealthValue(int value)
        {
            healthSlider.value -= value;
        }

        private void IncreaseHealthValue()
        {
            healthSlider.value += 1;
        }

        private void DecreaseBloodValue()
        {
            bloodSlider.value -= PlayerPreferences.BloodSpend;
        }

        private void IncreaseBloodValue(float value)
        {
            bloodSlider.value += value;
        }

        private void DecreaseBloodByValue(float value)
        {
            bloodSlider.value -= value;
        }
        
        private void CheckMenuInput()
        {
            if (Input.GetKeyDown(PlayerInGameInput.JournalKey))
            {
                diary.SetActive(false);
                
                if (journal.activeSelf)
                    journal.SetActive(false);
                
                else
                    journal.SetActive(true);
            }
            

            if (Input.GetKeyDown(PlayerInGameInput.DiaryKey))
            {
                journal.SetActive(false);
                
                if (diary.activeSelf)
                    diary.SetActive(false);
                
                else
                    diary.SetActive(true);
            }

            if (Input.GetKeyDown(PlayerInGameInput.EscapeKey))
            {
                journal.SetActive(false);
                diary.SetActive(false);
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void CheckMenuCloseInput()
        {
            if (Input.GetKeyDown(PlayerInGameInput.EscapeKey))
            {
                Debug.Log(pause.activeSelf);
                journal.SetActive(false);
                diary.SetActive(false);

                if (pause.activeSelf)
                {
                    pause.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }
}