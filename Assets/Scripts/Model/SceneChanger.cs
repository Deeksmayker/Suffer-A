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
            if (playerController == null)
                return;
            DontDestroyOnLoad(col.gameObject);
            _spawnPointName = gameObject.name;
            StartCoroutine(LoadRoom(playerController));
        }

        private IEnumerator LoadRoom(PlayerController playerController)
        {
            DontDestroyOnLoad(gameObject);
            var a = transitionTime;
            
            animator.SetTrigger("Start");
            PlayerPreferences.DisableControl();
            StartCoroutine(playerController.WalkToDirection(1));
            
            yield return new WaitForSeconds(a);

            var asyncLoader = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoader.isDone)
                yield return null;
            
            //playerController.transform.position = GameObject.Find(_spawnPointName).transform.position;
            
            
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(NewLoad());
        }

        private IEnumerator NewLoad()
        {
            Debug.Log(234);
            var player = GameObject.FindWithTag("Player");
            Debug.Log(GameObject.Find(_spawnPointName).transform.position);
            player.transform.position = GameObject.Find(_spawnPointName + "a").transform.position;
            animator.SetTrigger("Stop");
            Debug.Log(44);
            yield return new WaitForSeconds(transitionTime);
            
            PlayerPreferences.EnableControl();
        }
    }
}