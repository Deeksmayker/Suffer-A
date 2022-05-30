using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class FirstRoomCutscene : MonoBehaviour
    {
        [SerializeField] private GameObject video;
        private GameObject _vid;
        private IEnumerator Start()
        {
            _vid = Instantiate(video);
            _vid.GetComponent<VideoPlayer>().targetCamera = UnityEngine.Camera.main;
            yield return new WaitForSeconds(50);
            Destroy(_vid);
            Destroy(gameObject);
        }
    }
}