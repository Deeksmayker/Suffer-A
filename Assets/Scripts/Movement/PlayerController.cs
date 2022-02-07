using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Model;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class PlayerController : KinematicObject
    {
        [SerializeField] private float speed = 7;
        
        [SerializeField] private float jumpModifier = 1.5f;
        [SerializeField] private float jumpTakeOffSpeed = 7f;
        [SerializeField] private float jumpTime = 0.5f;
        [SerializeField] private float jumpDeceleration = 0.5f;

        [SerializeField] private float lungeSpeed = 7f;
        [SerializeField] private float lungeDuration = 0.2f;
        private int _currentLungeAirCount = Player.LungeAirCount;

        
        
        private Vector2 _move;
        

        private void Awake()
        {
            PlayerInput.OnJumpKeyDown.AddListener(StartJump);
            PlayerInput.OnJumpKeyUp.AddListener(StopJump);
            PlayerInput.OnLungeKeyDown.AddListener(StartLunge);
            GroundChecker.OnFloorMove.AddListener(MoveCharacterOnMovingFloor);
        }
        
        protected override void Update()
        {
            if (!Player.ControlEnabled)
            {
                return;
            }

            if (IsGrounded)
            {
                _currentLungeAirCount = Player.LungeAirCount;
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

        #region JumpLogic

        private void StartJump()
        {
            if (IsGrounded)
                StartCoroutine("Jump");
        }
        
        private IEnumerator Jump()
        {
            var duration = 0f;
            while (duration < jumpTime)
            {
                velocity.y = jumpTakeOffSpeed * jumpModifier;
                duration += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(SlowStopJump());
        }

        private IEnumerator SlowStopJump()
        {
            while (velocity.y > 0)
            {
                velocity.y *= jumpDeceleration;
                yield return new WaitForFixedUpdate();
            }
        }

        private void StopJump()
        {
            StopCoroutine("Jump");
            if (velocity.y > 0)
                velocity.y = 0;
        }

        #endregion

        private void StartLunge()
        {
            if (!IsGrounded)
            {
                if (_currentLungeAirCount == 0)
                    return;
                _currentLungeAirCount -= 1;
            }
            
            StartCoroutine("Lunge");
        }
        
        private IEnumerator Lunge()
        {
            Player.DisableControl();
            Bounce(new Vector2(PlayerInput.HorizontalRaw * lungeSpeed, PlayerInput.VerticalRaw * lungeSpeed / 2));
            yield return new WaitForSeconds(lungeDuration);
            Player.EnableControl();
        }

        private void MoveCharacterOnMovingFloor(Vector2 delta)
        {
            transform.position = new Vector3(transform.position.x + delta.x, transform.position.y + delta.y, transform.position.z);
        }
        
    }
}

