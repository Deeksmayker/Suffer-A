using System;
using System.Collections;
using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;
        [SerializeField] private Animator animator;
        [SerializeField] private float transitionTime;
        private string _spawnPointName;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var playerController = col.gameObject.GetComponent<PlayerController>();
            if (playerController == null || !PlayerPreferences.ControlEnabled)
                return;
            DontDestroyOnLoad(col.gameObject);
            _spawnPointName = gameObject.name;
            StartCoroutine(LoadRoom(playerController));
        }

        private IEnumerator LoadRoom(PlayerController playerController)
        {
            DontDestroyOnLoad(gameObject);

            animator.SetTrigger("Start");
            PlayerPreferences.DisableControl();
            StartCoroutine(playerController.WalkToDirection(PlayerPreferences.FaceRight ? 1 : -1));
            
            yield return new WaitForSeconds(transitionTime);

            var asyncLoader = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoader.isDone)
                yield return null;
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(OnSceneLoad());
        }

        private IEnumerator OnSceneLoad()
        {
            var player = GameObject.FindWithTag("Player");
            
            player.transform.position = GameObject.Find(_spawnPointName + "Spawn").transform.position;

            yield return new WaitForSeconds(transitionTime);
            
            PlayerPreferences.EnableControl();

            Destroy(gameObject);
        }
    }
}