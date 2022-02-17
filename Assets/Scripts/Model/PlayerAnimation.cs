using System;
using DefaultNamespace.Fight;
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
        public const string Attack = "attack";

        private PlayerController _player;
        private Animator _animator;

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
            _animator = GetComponent<Animator>();
            PlayerHorizontalAttack.OnHorizontalCanAttack.AddListener(SetAttack);
        }

        private void LateUpdate()
        {
            _animator.SetFloat(VelocityX, Math.Abs(_player.GetCurrentMove().x));
            _animator.SetFloat(VelocityY, _player.GetCurrentVelocity().y);
            _animator.SetBool(Grounded, PlayerPreferences.IsGrounded);
        }

        private void SetAttack(bool canAttack)
        {
            _animator.SetBool(Attack, canAttack);
        }
    }
}