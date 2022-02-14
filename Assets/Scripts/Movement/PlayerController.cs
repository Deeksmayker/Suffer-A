using System;
using System.Collections;
using DefaultNamespace;
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
        private int _currentLungeAirCount = PlayerPreferences.LungeAirCount;

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
            if (!PlayerPreferences.ControlEnabled)
            {
                return;
            }

            if (IsGrounded)
            {
                _currentLungeAirCount = PlayerPreferences.LungeAirCount;
            }
            
            HorizontalMove();
            base.Update();
        }

        private void LateUpdate()
        {
            Reflect();
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

        #region LungeLogic

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
            PlayerPreferences.DisableControl();
            Bounce(new Vector2(PlayerInput.HorizontalRaw * lungeSpeed, PlayerInput.VerticalRaw * lungeSpeed / 2));
            yield return new WaitForSeconds(lungeDuration);
            PlayerPreferences.EnableControl();
            StartCoroutine(SlowStopJump());
        }

        private void MoveCharacterOnMovingFloor(Vector2 delta)
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x + delta.x, currentPosition.y + delta.y, currentPosition.z);
        }

        #endregion

        #region Getters

        public Vector2 GetCurrentMove() => _move;
        public Vector2 GetCurrentVelocity() => velocity;

        #endregion

        
        private void Reflect()
        {
            if (_move.x > 0 && !PlayerPreferences.FaceRight ||
                _move.x < 0 && PlayerPreferences.FaceRight)
            {
                transform.localScale *= new Vector2(-1, 1);
                PlayerPreferences.FaceRight = !PlayerPreferences.FaceRight;
            }
        }
    }
}

