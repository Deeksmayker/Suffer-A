using System;
using System.Collections;
using DefaultNamespace.Fight;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class FireFloor : MonoBehaviour
    {
        [SerializeField] private float timeForTakeDamage;
        
        private Coroutine _coroutine;
        private bool _isRunning;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;

            other.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
            if (PlayerInGameInput.HorizontalRaw == 0)
            {
                if (!_isRunning)
                {
                    _coroutine = StartCoroutine(WaitForTime());
                    _isRunning = true;
                }
            }
            else
            {
                if (_isRunning)
                {
                    StopCoroutine(_coroutine);
                    _isRunning = false;
                }
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;
            other.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            StopCoroutine(_coroutine);
            _isRunning = false;
        }

        private IEnumerator WaitForTime()
        {
            var time = 0f;

            while (time < timeForTakeDamage)
            {
                time += Time.deltaTime;
                yield return null;
            }
            
            PlayerHealth.OnHitTaken.Invoke(1);
            _isRunning = false;
        } 
    }
}