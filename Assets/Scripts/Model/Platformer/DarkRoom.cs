using System;
using System.Collections;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    [RequireComponent(typeof(Animator))]
    public class DarkRoom : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null)
                return;
            
            _animator.SetBool("Inside", true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;
            
            _animator.SetBool("Inside", false);
        }
    }
}