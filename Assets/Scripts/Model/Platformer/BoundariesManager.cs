using System;
using Cinemachine;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class BoundariesManager : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner machine;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.GetComponent<PlayerController>())
                return;

            machine.m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
        }
    }
}