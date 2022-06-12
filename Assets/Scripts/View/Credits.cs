using System;
using System.Collections;
using Agava.YandexGames;
using Camera;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Awake()
        {
            ControllerBossAttack.OnBossKilled.AddListener(() => StartCoroutine(PlayCredits()));
        }

        public IEnumerator PlayCredits()
        {
            VideoAd.Show();
            
            canvas.SetActive(true);
            canvas.GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            Destroy(FindObjectOfType<CameraStuff>().gameObject);

            SceneManager.LoadScene(0);
        }
    }
}