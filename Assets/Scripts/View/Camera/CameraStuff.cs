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
    }
}