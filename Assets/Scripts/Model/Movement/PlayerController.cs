using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Fight;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class PlayerController : PlayerKinematicObject
    {
        [SerializeField] private float speed = 7;
        
        [SerializeField] private float jumpModifier = 1.5f;
        [SerializeField] private float jumpTakeOffSpeed = 7f;
        [SerializeField] private float jumpTime = 0.5f;
        [SerializeField] private float jumpDeceleration = 0.5f;

        [SerializeField] private float lungeSpeed = 7f;
        [SerializeField] private float lungeDuration = 0.2f;
        [SerializeField] private float lungeCooldown = 0.2f;

        [SerializeField] private float horizontalAttackBouncePower;
        [SerializeField] private float downAttackBouncePower;
        
        private bool _canLunge = true;
        private int _currentLungeAirCount = PlayerPreferences.MaxLungeAirCount;

        private Vector2 _move;
        

        private void Awake()
        {
            PlayerInGameInput.OnJumpKeyDown.AddListener(StartJump);
            PlayerInGameInput.OnJumpKeyUp.AddListener(StopJump);
            PlayerInGameInput.OnLungeKeyDown.AddListener(StartLunge);
            GroundChecker.OnFloorMove.AddListener(MoveCharacterOnMovingFloor);
            //PlayerAttack.OnEnemyHorizontalHit.AddListener(() => StartCoroutine(BounceOnHorizontalHit()));
            PlayerAttack.OnEnemyDownHit.AddListener(BounceOnDownHit);
        }
        
        protected override void Update()
        {
            if (!PlayerPreferences.ControlEnabled)
            {
                _move = Vector2.zero;
                return;
            }

            if (PlayerPreferences.IsGrounded)
            {
                _currentLungeAirCount = PlayerPreferences.MaxLungeAirCount;
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
            if (Math.Abs(_move.x) > 0 && Math.Abs(PlayerInGameInput.HorizontalRaw) > 0 && Math.Abs(targetVelocity.x) > speed)
            {
                var deceleration = 0.995f;
                targetVelocity.x *= deceleration;
            }
            else
            {
                targetVelocity = _move * speed;
            }
        }

        private void HorizontalMove()
        {
            _move.x = PlayerInGameInput.HorizontalRaw;
        }

        #region JumpLogic

        private void StartJump()
        {
            if (PlayerPreferences.IsGrounded)
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

            StartCoroutine(SlowStopJump(jumpDeceleration));
        }

        private IEnumerator SlowStopJump(float deceleration)
        {
            while (velocity.y > 0)
            {
                velocity.y *= deceleration;
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
            if (!PlayerPreferences.IsGrounded)
            {
                if (_currentLungeAirCount == 0)
                    return;
                _currentLungeAirCount -= 1;
            }
            
            StartCoroutine("Lunge");
        }
        
        private IEnumerator Lunge()
        {
            if (!_canLunge)
                yield break;
            
            Vector2 direction = Vector2.zero;
            
            if (PlayerInGameInput.HorizontalRaw == 0 && PlayerInGameInput.VerticalRaw == 0)
                direction = new Vector2(PlayerPreferences.FaceRight ? lungeSpeed : -lungeSpeed, 0);
            else
                direction = new Vector2(PlayerInGameInput.HorizontalRaw * lungeSpeed,
                    PlayerInGameInput.VerticalRaw * lungeSpeed / 2);
            
            PlayerPreferences.DisableControl();
            Bounce(direction);
            yield return new WaitForSeconds(lungeDuration);
            PlayerPreferences.EnableControl();
            StartCoroutine(SlowStopJump(jumpDeceleration));
            _canLunge = false;
            yield return new WaitForSeconds(lungeCooldown);
            _canLunge = true;
        }
        
        #endregion

        private IEnumerator BounceOnHorizontalHit()
        {
            var delay = 0.1f;
            PlayerPreferences.DisableControl();
            Bounce(new Vector2(PlayerPreferences.FaceRight ? -horizontalAttackBouncePower : horizontalAttackBouncePower, 0));
            yield return new WaitForSeconds(delay);
            
            PlayerPreferences.EnableControl();
        }

        private void BounceOnDownHit()
        {
            _currentLungeAirCount = PlayerPreferences.MaxLungeAirCount;

            Bounce(new Vector2(0, downAttackBouncePower));
            StartCoroutine(SlowStopJump(jumpDeceleration));
        }

        #region Getters

        public Vector2 GetCurrentMove() => _move;
        public Vector2 GetCurrentVelocity() => velocity;

        #endregion

        
        private void Reflect()
        {
            if (_move.x > 0 && !PlayerPreferences.FaceRight ||
                _move.x < 0 && PlayerPreferences.FaceRight)
            {
                transform.Rotate(0f, 180f, 0f);
                PlayerPreferences.FaceRight = !PlayerPreferences.FaceRight;
            }
        }
        
        private void MoveCharacterOnMovingFloor(Vector2 delta)
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x + delta.x, currentPosition.y + delta.y, currentPosition.z);
        }

        public IEnumerator WalkToDirection(int direction)
        {
            while (!PlayerPreferences.ControlEnabled)
            {
                _move.x = direction;
                yield return null;
            }
        }

        public IEnumerator FlyUp(float power, float time, float deceleration)
        {
            var duration = 0f;

            while (duration <= time)
            {
                duration += Time.deltaTime;
                velocity.y = power;
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(SlowStopJump(deceleration));
        }
    }
}

