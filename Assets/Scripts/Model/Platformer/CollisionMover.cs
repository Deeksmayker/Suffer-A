using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class CollisionMover : MonoBehaviour
    {
        [SerializeField] private Vector2 startPoint, endPoint;
        [SerializeField] private float speed = 1;
        
        private void OnCollisionEnter2D()
        {
            StartCoroutine(Utils.MoveToTarget(transform, endPoint, speed));
        }
    }
}