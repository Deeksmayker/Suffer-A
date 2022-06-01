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
        [SerializeField] private GameObject video;
        private GameObject vid;

        private IEnumerator Start()
        {
#if UNITY_WEBGL || !UNITY_EDITOR
            yield return YandexGamesSdk.WaitForInitialization();
            InterestialAd.Show();
#endif

            vid = Instantiate(video);
            vid.GetComponent<VideoPlayer>().targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(8);
            Destroy(vid);
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                Destroy(vid);
                StopAllCoroutines();
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