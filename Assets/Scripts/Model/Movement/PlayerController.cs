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
        [SerializeField] private float maxSpeed = 7;
        [SerializeField] private float acceleration;
        
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

        public static UnityEvent OnAirJerk = new UnityEvent();
        public static UnityEvent OnRoll = new UnityEvent();

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
            
            PlayerInGameInput.OnJumpKeyDown.AddListener(StartJump);
            PlayerInGameInput.OnJumpKeyUp.AddListener(StopJump);
            PlayerInGameInput.OnLungeKeyDown.AddListener(StartLunge);
            GroundChecker.OnFloorMove.AddListener(MoveCharacterOnMovingFloor);
            //PlayerAttack.OnEnemyHorizontalHit.AddListener(() => StartCoroutine(BounceOnHorizontalHit()));
            PlayerAttack.OnEnemyDownHit.AddListener(BounceOnDownHit);
            PlayerAttack.OnEnemyHorizontalHit.AddListener((b) => StartCoroutine(BounceOnHorizontalHit(b)));
        }
        
        protected override void Update()
        {
            if (PlayerPreferences.IsGrounded)
            {
                _currentLungeAirCount = PlayerPreferences.MaxLungeAirCount;
            }

            if (PlayerPreferences.ControlEnabled)
            {
                HorizontalMove();
                base.Update();
            }
            
            else if (PlayerPreferences.InTransition)
                base.Update();
        }

        private void LateUpdate()
        {
            Reflect();
        }

        private bool _speedIsHightCoroutine;
        protected override void ComputeVelocity()
        {
            var inMove = Math.Abs(_move.x) > 0;
            var needToMove = Math.Abs(PlayerInGameInput.HorizontalRaw) > 0;
            
            if (inMove && needToMove && Math.Abs(targetVelocity.x) > maxSpeed)
            {
                if (!_speedIsHightCoroutine)
                {
                    _speedIsHightCoroutine = true;
                    StartCoroutine(HighSpeed());
                }
            }
            else
            {
                _speedIsHightCoroutine = false;
                targetVelocity = _move * maxSpeed;
            }
        }

        private IEnumerator HighSpeed()
        {
            while (_speedIsHightCoroutine)
            {
                var deceleration = 0.85f;
                targetVelocity.x *= deceleration;
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        private void HorizontalMove()
        {
            bool isAccelerationOrMaxSpeed = Math.Abs(PlayerInGameInput.HorizontalRaw) == 1;
            
            _move.x = Mathf.Clamp(PlayerInGameInput.Horizontal * (isAccelerationOrMaxSpeed ? acceleration : 1/acceleration), -1, 1);
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

            while (PlayerPreferences.InTransition)
            {
                velocity.y = jumpTakeOffSpeed * jumpModifier / 3;
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
            if (!_canLunge)
                return;
            
            if (!PlayerPreferences.IsGrounded)
            {
                if (_currentLungeAirCount == 0)
                    return;
                _currentLungeAirCount -= 1;
                OnAirJerk.Invoke();
            }
            
            else
                OnRoll.Invoke();
            
            StartCoroutine("Lunge");
        }
        
        private IEnumerator Lunge()
        {
            yield return new WaitForFixedUpdate();
            Vector2 direction = Vector2.zero;
            
            if (PlayerInGameInput.HorizontalRaw == 0 && PlayerInGameInput.VerticalRaw == 0)
                direction = new Vector2(PlayerPreferences.FaceRight ? lungeSpeed : -lungeSpeed, 0);
            else
                direction = new Vector2(PlayerInGameInput.HorizontalRaw * lungeSpeed,
                    PlayerInGameInput.VerticalRaw * lungeSpeed / 2);
            
            PlayerPreferences.DisableControl();
            Bounce(direction);
            yield return new WaitForSeconds(lungeDuration);
            if (!PlayerPreferences.InTransition)
                PlayerPreferences.EnableControl();
            StartCoroutine(SlowStopJump(jumpDeceleration));
            _canLunge = false;
            yield return new WaitForSeconds(lungeCooldown);
            _canLunge = true;
        }
        
        #endregion

        private IEnumerator BounceOnHorizontalHit(bool powerAttack)
        {
            if (!powerAttack)
                yield break;
            
            var delay = 0.1f;
            PlayerPreferences.DisableControl();
            var bounceVector = Vector2.right * (PlayerPreferences.FaceRight
                ? -horizontalAttackBouncePower
                : horizontalAttackBouncePower);
            
            
            /*if (!PlayerPreferences.IsGrounded)
            {
                _currentLungeAirCount = PlayerPreferences.MaxLungeAirCount;

                bounceVector.y = downAttackBouncePower * 0.5f;
            }*/
            
            Bounce(bounceVector);
            /*StartCoroutine(SlowStopJump(jumpDeceleration));*/

            yield return new WaitForSeconds(delay);

            PlayerPreferences.EnableControl();
        }

        private void BounceOnDownHit(bool powerAttack)
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
            if (!PlayerPreferences.CanMove || !PlayerPreferences.ControlEnabled)
                return;
            
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

