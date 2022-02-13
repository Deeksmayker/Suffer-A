using System;
using DefaultNamespace;
using Movement;
using UnityEngine;
using PlayerPrefs = DefaultNamespace.PlayerPrefs;

namespace Mechanics
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag != "Player")
                return;
            col.GetComponent<PlayerController>().Teleport(PlayerPrefs.SpawnPoint);
        }
    }
}