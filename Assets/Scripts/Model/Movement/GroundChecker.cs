using System;
using System.Collections;
using DefaultNamespace;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private float extraHeightText = 0.5f;
        
        private bool _coroutineRunning = false;
        private Vector2 _previousFloorPosition;

        public static UnityEvent<Vector2> OnFloorMove = new UnityEvent<Vector2>();

        private RaycastHit2D _rayCastHit;

        private void Awake()
        {
            _previousFloorPosition = Vector2.zero;
            StartCoroutine(CheckFloor());
        }

        private void Update()
        {
            CheckGrounded();
            
        }

        private void CheckGrounded()
        {
            _rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down,
                extraHeightText, floorLayer);

            //Color rayColor = _rayCastHit.collider != null ? Color.green : Color.red;

            var nowGrounded = _rayCastHit.collider != null;

            if (PlayerPreferences.IsGrounded && !nowGrounded && !_coroutineRunning)
                StartCoroutine("MakeCoyoteTime");
            else if (!_coroutineRunning)
                PlayerPreferences.IsGrounded = nowGrounded;
        }

        private IEnumerator MakeCoyoteTime()
        {
            _coroutineRunning = true;
            yield return new WaitForSeconds(PlayerPreferences.CoyoteTime);
            PlayerPreferences.IsGrounded = false;
            _coroutineRunning = false;
        }

        private IEnumerator CheckFloor()
        {
            while (true)
            {
                if (PlayerPreferences.IsGrounded && _rayCastHit)
                {
                    if (_previousFloorPosition == Vector2.zero)
                        _previousFloorPosition = _rayCastHit.collider.transform.position;
                    CheckMovingFloor(_rayCastHit.collider);
                    _previousFloorPosition = _rayCastHit.collider.transform.position;
                }

                else
                {
                    _previousFloorPosition = Vector2.zero;
                }

                yield return new WaitForFixedUpdate();
            }
        }
        
        private void CheckMovingFloor(Collider2D floor)
        {
            if (_previousFloorPosition.Equals(floor.transform.position))
                return;

            var deltaMove = new Vector2(floor.transform.position.x - _previousFloorPosition.x,
                floor.transform.position.y - _previousFloorPosition.y);
            OnFloorMove.Invoke(deltaMove);
        }
    }
}