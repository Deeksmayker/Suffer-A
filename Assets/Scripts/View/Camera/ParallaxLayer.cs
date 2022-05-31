using System;
using UnityEngine;

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
        public Vector2 movementScale = Vector2.one;

        private Transform _camera;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main.transform;
        }

        void LateUpdate()
        {
            transform.position = Vector2.Scale(_camera.position, movementScale);
        }
    }
}