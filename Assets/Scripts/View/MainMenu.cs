using System;
using System.Collections;
using Agava.YandexGames;
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
            LoadFirstScene();
            
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"zagruzka.mp4");

            videoPlayer.Play();
            videoPlayer.targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(8);
            Destroy(videoPlayer);
            Destroy(GameObject.Find("Cube"));
            
            
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

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}