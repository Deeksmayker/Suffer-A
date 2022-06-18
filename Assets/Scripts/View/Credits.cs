using System;
using System.Collections;
using Camera;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        public static UnityEvent OnGameEnd = new UnityEvent();

        private void Awake()
        {
            ControllerBossAttack.OnBossKilled.AddListener(() => StartCoroutine(PlayCredits()));
        }

        public IEnumerator PlayCredits()
        {
            OnGameEnd.Invoke();
            canvas.SetActive(true);
            canvas.GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            Destroy(FindObjectOfType<CameraStuff>().gameObject);

            SceneManager.LoadScene(0);
        }
    }
}