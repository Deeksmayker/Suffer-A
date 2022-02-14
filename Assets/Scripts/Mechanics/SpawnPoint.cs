using System;
using DefaultNamespace;
using UnityEngine;

namespace Mechanics
{
    public class SpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
                PlayerPreferences.SetSpawnPoint(transform.position);
        }
    }
}