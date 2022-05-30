using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class PanicCutsceen : MonoBehaviour
    {
        [SerializeField] private GameObject video;
        private GameObject _vid;
        private IEnumerator Start()
        {
            while (PlayerPreferences.InTransition)
            {
                yield return null;
            }
            
            PlayerPreferences.CanMove = false;
            _vid = Instantiate(video);
            _vid.GetComponent<VideoPlayer>().targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(47);
            PlayerPreferences.CanMove = true;
            Destroy(_vid);
            Destroy(gameObject);
        }
    }
}