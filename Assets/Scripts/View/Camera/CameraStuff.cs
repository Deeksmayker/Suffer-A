using System;
using UnityEngine;

namespace Camera
{
    public class CameraStuff : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetAudio(bool flag)
        {
            GetComponentInChildren<AudioListener>().enabled = flag;
        }
    }
}