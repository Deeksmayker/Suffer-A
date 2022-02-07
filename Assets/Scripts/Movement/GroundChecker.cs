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
        private PlayerController _player;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private float extraHeightText = 0.5f;
        
        private bool _coroutineRunning = false;
        private Vector2 _previousFloorPosition;

        public static UnityEvent<Vector2> OnFloorMove = new UnityEvent<Vector2>();

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
            _previousFloorPosition = Vector2.zero;
        }

        private void Update()
        {
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            var rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down,
                extraHeightText, floorLayer);

            Color rayColor = rayCastHit.collider != null ? Color.green : Color.red;

            var nowGrounded = rayCastHit.collider != null;

            if (_player.IsGrounded && !nowGrounded && !_coroutineRunning)
                StartCoroutine("MakeCoyoteTime");
            else if (!_coroutineRunning)
                _player.IsGrounded = nowGrounded;

            if (_player.IsGrounded && rayCastHit)
            {
                if (_previousFloorPosition == Vector2.zero)
                    _previousFloorPosition = rayCastHit.collider.transform.position;
                CheckMovingFloor(rayCastHit.collider);
                _previousFloorPosition = rayCastHit.collider.transform.position;
            }

            else
            {
                _previousFloorPosition = Vector2.zero;
            }
        }

        private IEnumerator MakeCoyoteTime()
        {
            _coroutineRunning = true;
            yield return new WaitForSeconds(Player.CoyoteTime);
            _player.IsGrounded = false;
            _coroutineRunning = false;
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