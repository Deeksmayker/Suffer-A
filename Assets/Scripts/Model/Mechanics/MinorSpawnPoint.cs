using System;
using DefaultNamespace;
using Movement;
using UnityEngine;

namespace Mechanics
{
    public class MinorSpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null)
                return;
            
            PlayerPreferences.MinorSpawnPoint = transform.position;
        }
    }
}