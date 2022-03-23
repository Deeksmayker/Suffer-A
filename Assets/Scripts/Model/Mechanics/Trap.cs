using System;
using DefaultNamespace;
using Movement;
using UnityEngine;

namespace Mechanics
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag != "Player")
                return;
            col.GetComponent<PlayerController>().Teleport(PlayerPreferences.SpawnPoint);
        }
    }
}