using System;
using System.Collections;
using Camera;
using DefaultNamespace.Fight;
using Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
        [SerializeField] private GameObject settings;

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
            
        }

        private void CheckMenuCloseInput()
        {
            if (Input.GetKeyDown(PlayerInGameInput.EscapeKey))
            {
                if (journal.activeSelf || diary.activeSelf || settings.activeSelf)
                {
                    journal.SetActive(false);
                    diary.SetActive(false);
                    settings.SetActive(false);
                    return;
                }

                if (pause.activeSelf)
                {
                    pause.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    journal.SetActive(false);
                    diary.SetActive(false);
                    pause.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }

        public void ClosePause()
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }

        public void ReturnToMainMenu()
        {
            PlayerPrefs.SetInt("attack", PlayerPreferences.AttackAvailable ? 1 : 0);
            PlayerPrefs.SetInt("horizontal", PlayerPreferences.HorizontalAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("up", PlayerPreferences.UpAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("down", PlayerPreferences.DownAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("airLunge", PlayerPreferences.MaxLungeAirCount);
            PlayerPrefs.SetInt("scene", PlayerPreferences.CurrentSceneIndex);
            PlayerPrefs.SetInt("health", PlayerPreferences.CurrentHealth);
            PlayerPrefs.SetFloat("blood", PlayerPreferences.CurrentBlood);
            PlayerPrefs.SetFloat("majorX", PlayerPreferences.MajorSpawnPoint.x);
            PlayerPrefs.SetFloat("majorY", PlayerPreferences.MajorSpawnPoint.y);
            PlayerPrefs.Save();

            Time.timeScale = 1;
            Destroy(FindObjectOfType<PlayerController>().gameObject);
            Destroy(FindObjectOfType<CameraStuff>().gameObject);
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}