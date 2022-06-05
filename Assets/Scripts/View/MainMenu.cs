using System;
using System.Collections;
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
            vid = Instantiate(video);
            vid.GetComponent<VideoPlayer>().targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(8);
            Destroy(vid);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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