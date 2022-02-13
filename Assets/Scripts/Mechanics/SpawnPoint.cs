using System;
using DefaultNamespace;
using UnityEngine;
using PlayerPrefs = DefaultNamespace.PlayerPrefs;

namespace Mechanics
{
    public class SpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
                PlayerPrefs.SetSpawnPoint(transform.position);
        }
    }
}