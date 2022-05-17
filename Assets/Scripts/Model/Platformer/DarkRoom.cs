using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    [RequireComponent(typeof(Animator))]
    public class DarkRoom : MonoBehaviour
    {
        [NonSerialized] public Animator Animator;

        [SerializeField] private List<GameObject> nearestDarkRooms = new List<GameObject>();

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerController>() == null) 
                return;
             
            Animator.SetBool("Inside", true);
            foreach (var room in nearestDarkRooms)
            {
                room.GetComponent<DarkRoom>().Animator.SetBool("Inside", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;
            
            Animator.SetBool("Inside", false);
            foreach (var room in nearestDarkRooms)
            {
                room.GetComponent<DarkRoom>().Animator.SetBool("Inside", false);
            }
        }
    }
}