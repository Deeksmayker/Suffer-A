using System;
using System.Collections;
using Cinemachine;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class BoundariesManager : MonoBehaviour
    {
        private CinemachineConfiner machine;

        private void Start()
        {
            machine = GameObject.FindObjectOfType<CinemachineConfiner>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.GetComponent<PlayerController>())
                return;

            machine.m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
        }

        private void OnDestroy()
        {
            machine.m_BoundingShape2D = null;
        }
    }
}