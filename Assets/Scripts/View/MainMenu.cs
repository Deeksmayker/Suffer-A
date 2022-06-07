using System;
using System.Collections;
using Agava.YandexGames;
using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        
        private IEnumerator Start()
        {
            yield return YandexGamesSdk.WaitForInitialization();
            InterestialAd.Show();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && videoPlayer != null)
            {
                Destroy(videoPlayer);
                Destroy(GameObject.Find("Cube"));
                StopAllCoroutines();
                InterestialAd.Show();
            }
        }

        public void LoadFirstScene()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadPlayerPrefsS()
        {
            StartCoroutine(LoadPlayerPrefs());
        }
        
        private IEnumerator LoadPlayerPrefs() 
        {
            PlayerPreferences.CurrentSceneIndex = PlayerPrefs.GetInt("scene");
            if (PlayerPreferences.CurrentSceneIndex == 0)
                yield break;
            DontDestroyOnLoad(gameObject);
            PlayerPreferences.AttackAvailable = PlayerPrefs.GetInt("attack") == 1;
            PlayerPreferences.HorizontalAbilityAvailable = PlayerPrefs.GetInt("horizontal") == 1;
            PlayerPreferences.UpAbilityAvailable = PlayerPrefs.GetInt("up") == 1;
            PlayerPreferences.DownAbilityAvailable = PlayerPrefs.GetInt("down") == 1;
            PlayerPreferences.MaxLungeAirCount = PlayerPrefs.GetInt("airLunge");
            PlayerPreferences.CurrentHealth = PlayerPrefs.GetInt("health");
            PlayerPreferences.CurrentBlood = PlayerPrefs.GetFloat("blood");
            PlayerPreferences.MajorSpawnPoint =
                new Vector2(PlayerPrefs.GetFloat("majorX"), PlayerPrefs.GetFloat("majorY"));

            LoadFirstScene();
            yield return null;
            SceneManager.LoadScene(PlayerPreferences.CurrentSceneIndex);

            GameObject.FindWithTag("Player").gameObject.transform.position = PlayerPreferences.MajorSpawnPoint;
            
            Destroy(gameObject);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}