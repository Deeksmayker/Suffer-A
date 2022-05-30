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

        private Animator _fireFloorEffectAnimator;
        
        private Coroutine _coroutine;
        private bool _isRunning;

        private void Awake()
        {
            _fireFloorEffectAnimator = GameObject.Find("FireFloorEffect").GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null)
                return;
            
            _fireFloorEffectAnimator.SetBool("fire", true);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == null)
                return;

            collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
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

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() == null)
                return;
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            StopCoroutine(_coroutine);
            _isRunning = false;
            _fireFloorEffectAnimator.SetBool("fire", false);
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