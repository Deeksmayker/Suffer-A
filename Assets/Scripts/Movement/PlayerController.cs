using System;
using System.Collections;
using DefaultNamespace;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : KinematicObject
    {
        [SerializeField] private float speed = 7;
        private Vector2 _move;

        private void Awake()
        {
            PlayerInput.OnJumpKeyDown.AddListener(Jump);
        }
        
        protected override void Update()
        {
            if (!Player.ControlEnabled)
            {
                return;
            }
            
            HorizontalMove();
            base.Update();
        }

        protected override void ComputeVelocity()
        {
            targetVelocity = _move * speed;
        }

        private void HorizontalMove()
        {
            _move.x = PlayerInput.HorizontalRaw;
        }

        private void Jump()
        {
            
        }
    }
}

