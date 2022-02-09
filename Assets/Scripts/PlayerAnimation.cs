using System;
using Movement;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        public const string VelocityX = "velocityX";
        public const string VelocityY = "velocityY";
        public const string Grounded = "grounded";

        private PlayerController _player;
        private Animator _animator;

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            _animator.SetFloat(VelocityX, Math.Abs(_player.GetCurrentMove().x));
            _animator.SetFloat(VelocityY, _player.GetCurrentVelocity().y);
            _animator.SetBool(Grounded, _player.IsGrounded);
            
            Reflect();
        }

        private void Reflect()
        {
            if (_player.GetCurrentMove().x > 0 && !Player.FaceRight ||
                _player.GetCurrentMove().x < 0 && Player.FaceRight)
            {
                transform.localScale *= new Vector2(-1, 1);
                Player.FaceRight = !Player.FaceRight;
            }
        }
    }
}