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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<PlayerController>())
                return;
            
            if (machine.m_BoundingShape2D == null)
                machine.m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<PlayerController>())
                return;

            machine.m_BoundingShape2D = null;
        }

        private void OnDestroy()
        {
            machine.m_BoundingShape2D = null;
        }
    }
}