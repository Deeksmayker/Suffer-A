using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class PanicCutsceen : MonoBehaviour
    {

        public UnityEvent onRoomEnter = new UnityEvent();
        
        private void Start()
        {
            onRoomEnter.Invoke();
        }
    }
}