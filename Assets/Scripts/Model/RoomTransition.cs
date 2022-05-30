using System;
using System.Collections;
using System.Linq;
using Cinemachine;
using DefaultNamespace.Platformer;
using Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RoomTransition : MonoBehaviour
    {
        private enum Direction
        {
            Right = 1,
            Left = -1,
            Stay = 0
        }
        
        [SerializeField] private int sceneIndex;
        private Animator _blackScreen;
        [SerializeField] private float transitionTime;
        [SerializeField] private Direction moveDirection;
        private string _transitionName;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            var playerController = col.gameObject.GetComponent<PlayerController>();
            if (playerController == null || PlayerPreferences.InTransition)
                return;
            PlayerPreferences.InTransition = true;
            DontDestroyOnLoad(playerController.gameObject);
            _transitionName = gameObject.name;
            StartCoroutine(LoadRoom(playerController));
        }

        private IEnumerator LoadRoom(PlayerController playerController)
        {
            DontDestroyOnLoad(gameObject);

            _blackScreen.SetBool("fade", true);
            PlayerPreferences.DisableControl();
            StartCoroutine(playerController.WalkToDirection((int)moveDirection));
            
            yield return new WaitForSeconds(transitionTime);

            var asyncLoader = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoader.isDone)
                yield return null;
        }

        private void Start()
        {
            _blackScreen = GameObject.Find("MinorLoadScreen").GetComponent<Animator>();
            
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
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = 
                FindObjectOfType<BoundariesManager>().GetComponent<PolygonCollider2D>();
        
            _blackScreen.SetBool("fade", false);
            var player = GameObject.FindWithTag("Player");
            
            player.transform.position = GameObject.Find(_transitionName + 1).transform.position;

            yield return new WaitForSeconds(transitionTime);
            
            PlayerPreferences.EnableControl();
            PlayerPreferences.InTransition = false;

            Destroy(gameObject);
        }
    }
}