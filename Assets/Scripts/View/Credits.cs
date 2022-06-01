using System.Collections;
using Camera;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private GameObject video;
        private GameObject _vid;
        
        public IEnumerator PlayCredits()
        {
            _vid = Instantiate(video);
            _vid.GetComponent<VideoPlayer>().targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(24);
            Destroy(_vid);
            Destroy(FindObjectOfType<CameraStuff>().gameObject);

            SceneManager.LoadScene(0);
        }
    }
}