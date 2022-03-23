﻿using UnityEngine;

namespace Camera
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        /// <summary>
        /// Movement of the layer is scaled by this value.
        /// </summary>
        public Vector3 movementScale = Vector3.one;

        [SerializeField] private Transform _camera;

        void LateUpdate()
        {
            transform.position = Vector3.Scale(_camera.position, movementScale);
        }
    }
}