using System;
using System.Collections;
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

            StartCoroutine(TeleportPlayerWithBlackScreen(col));
        }

        private IEnumerator TeleportPlayerWithBlackScreen(Collider2D col)
        {
            var blackScreenAnimator = GameObject.FindWithTag("DarkScreen").GetComponent<Animator>();
            
            blackScreenAnimator.SetTrigger("Start");

            yield return new WaitForSeconds(0.5f);

            if (PlayerPreferences.CurrentHealth == 0)
                yield break;
            
            blackScreenAnimator.SetTrigger("Stop");
            
            col.GetComponent<PlayerController>().Teleport(PlayerPreferences.MinorSpawnPoint);

        }
    }
}