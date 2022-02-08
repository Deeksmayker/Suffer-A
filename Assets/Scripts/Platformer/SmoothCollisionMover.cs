using System;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class SmoothCollisionMover : MonoBehaviour
    {
        [SerializeField] private Vector3 target;
        [SerializeField] private float speed = 1;

        private void OnCollisionEnter2D()
        {
            StartCoroutine(Utils.SmoothMoveToTarget(transform, target, speed));
        }
    }
}