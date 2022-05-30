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
            if (PlayerPreferences.CurrentHealth == 0)
                yield break;
            var blackScreenAnimator = GameObject.FindWithTag("DarkScreen").GetComponent<Animator>();
            
            blackScreenAnimator.SetBool("fade", true);
            PlayerPreferences.CanMove = false;

            yield return new WaitForSeconds(0.5f);

            if (PlayerPreferences.CurrentHealth == 0)
                yield break;
            
            blackScreenAnimator.SetBool("fade", false);
            PlayerPreferences.CanMove = true;
            
            col.GetComponent<PlayerController>().Teleport(PlayerPreferences.MinorSpawnPoint);

        }
    }
}