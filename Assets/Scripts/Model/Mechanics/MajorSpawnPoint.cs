using System;
using DefaultNamespace;
using Movement;
using UnityEngine;

namespace Mechanics
{
    public class MajorSpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null)
                return;

            PlayerPreferences.MajorSpawnPoint = transform.position;
        }
    }
}