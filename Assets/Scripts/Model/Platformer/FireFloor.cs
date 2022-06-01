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
        
        private bool _onFire;

        private void Awake()
        {
            _counter = timeForTakeDamage;
            _fireFloorEffectAnimator = GameObject.Find("FireFloorEffect").GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null)
                return;
            
            _fireFloorEffectAnimator.SetBool("fire", true);
            _onFire = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() == null)
                return;
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            _onFire = false;
            _fireFloorEffectAnimator.SetBool("fire", false);
        }

        private float _counter;
        
        private void Update()
        {
            if (PlayerInGameInput.HorizontalRaw != 0)
            {
                _counter = timeForTakeDamage;
                return;
            }
            
            if (_onFire)
            {
                if (_counter <= 0)
                {
                    PlayerHealth.OnHitTaken.Invoke(1);
                    _counter = timeForTakeDamage;
                }

                else
                {
                    _counter -= Time.deltaTime;
                }
            }

            else
            {
                _counter = timeForTakeDamage;
            }
        }
    }
}